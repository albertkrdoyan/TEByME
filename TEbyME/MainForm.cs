/*
 * Created by SharpDevelop.
 * User: User
 * Date: 04.11.2024
 * Time: 22:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TEbyME
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>

    public partial class MainForm : Form
    {
        /// turnes off selection blue area animation
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0x0B;
        /// end

        private string filepath;
        private bool text_changed, is_search_replace_window_open, is_search_popup_window, sw_first_time_load, out_of_sw_move_interval, new_file;

        KeyPressEventArgs key_press;
        string deletion_text = "";

        private struct Themes
        {
            public string name;
        }

        private struct SWindowMove
        {
            public bool is_swindow_mouse_down;
            public int dx, dy;
            public int mouse_x, mouse_y;
        }

        private struct SearchIN
        {
            public LinkedListNode<int> current;
            public LinkedList<int> indices;
            public int search_text_length;
            public bool show_next;
        }

        private Form searchWindow;
        private SWindowMove swm;
        private SearchIN si;
        private Themes theme;

        /// undo redo section
        private struct UndoRedo
        {
            public string data, replace;
            public int st_index;
            public int mode;

            public UndoRedo(string d, int s, int m, string r) /// m // 1 adding, 2 reading, 3 both, 4 replace all
            {
                replace = r;
                data = d;
                st_index = s;
                mode = m;
            }
        }
        Stack<UndoRedo> undos;
        Stack<UndoRedo> redos;

        int cursor_location_offset;
        bool ctrlZ, ctrlY, ctrlX;
        /// end of undo redo section

        private Counters counter;

        public MainForm(string path)
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //

            PathInit(path);
            OtherInits();
        }

        private void Reset()
        {
            undos = new Stack<UndoRedo>();
            redos = new Stack<UndoRedo>();
            cursor_location_offset = 0;
            ctrlZ = ctrlY = false;

            findPrevBtn.Enabled = findNextBtn.Enabled = false;
            replaceTB.Text = searchTB.Text = "";
            si = new MainForm.SearchIN();
        }

        private void PathInit(string path)
        {
            if (path != string.Empty)
            {
                filepath = path;
                textArea.Text = File.ReadAllText(filepath);
                textArea.SelectionStart = textArea.TextLength;
                text_changed = false;

                fileNameLabel.Text = "";
                int last_slash = -1;
                for (int i = 0; i < filepath.Length; ++i)
                {
                    if (filepath[i] == '\\')
                        last_slash = i;
                }
                for (int i = last_slash + 1; i < filepath.Length; ++i)
                    fileNameLabel.Text += filepath[i];
            }
            else
            {
                filepath = "";
                text_changed = false;
            }
        }

        private void OtherInits()
        {
            is_search_replace_window_open = swm.is_swindow_mouse_down = out_of_sw_move_interval = false;
            is_search_popup_window = sw_first_time_load = new_file = true;

            swm = new MainForm.SWindowMove();
            si = new MainForm.SearchIN();
            theme = new MainForm.Themes();
            theme.name = "default";

            undos = new Stack<UndoRedo>();
            redos = new Stack<UndoRedo>();
            cursor_location_offset = 0;
            ctrlZ = ctrlY = false;

            int hei = 113, wid = 663;
            searchWindow = new Form()
            {
                Size = new Size(wid, hei),
                MaximumSize = new Size(wid, hei),
                MinimumSize = new Size(wid, hei),
                Font = new Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Name = "searchWindow",
                ShowIcon = false,
                ControlBox = false,
                StartPosition = FormStartPosition.Manual,
                FormBorderStyle = FormBorderStyle.None
            };
            searchWindow.Load += new System.EventHandler(this.SForm_Load);

            findPrevBtn.Enabled = findNextBtn.Enabled = false;

            this.closeSearch.MouseClick += CloseSearch_Click;
            this.minMaxSearch.MouseClick += MinMaxSearch_Click;

            this.title.MouseDown += Sform_mouse_down;
            this.title.MouseUp += Sform_mouse_up;
            this.title.MouseMove += Sform_move;

            this.findBtn.MouseClick += FindBtn_MouseClick;
            this.findNextBtn.MouseClick += FindNextBtn_MouseClick;
            this.findPrevBtn.MouseClick += FindPrevBtn_MouseClick;

            this.replaceBtn.MouseClick += ReplaceBtn_MouseClick;
            this.replaceAllBtn.MouseClick += ReplaceAllBtn_MouseClick;

            this.clearBtn.MouseClick += ClearBtn_MouseClick;

            counter = new Counters(" ,./;[]\\!@#$%^&*()_+-=<>?:\"{}|`~\r\n\t", "\r\n");

            counter.CountBFC(textArea.Text);
            counterLabel.Text = counter.GetInfo();
        }

        private void ClearBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            replaceTB.Text = searchTB.Text = "";
            lorwecaseCHB.Checked = false;
            findNextBtn.Enabled = findPrevBtn.Enabled = false;

            si = new MainForm.SearchIN();
        }

        private void ReplaceAllBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || replaceTB.TextLength == 0) return;

            if (si.current == null)
                FindBtn_MouseClick(Keys.Enter, null);

            if (si.current == null)
                return;

            for (LinkedListNode<int> curr = si.indices.Last; curr != null; curr = curr.Previous)
            {
                textArea.Select(curr.Value, si.search_text_length);
                undos.Push(new UndoRedo(replaceTB.Text, curr.Value, 4, textArea.SelectedText));
                textArea.SelectedText = replaceTB.Text;
            }

            si = new SearchIN();
        }

        private void ReplaceBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || replaceTB.TextLength == 0) return;

            if (si.current == null)
                FindBtn_MouseClick(Keys.Enter, null);

            if (si.current == null || si.indices.Count == 0)
                return;

            int offset = replaceTB.TextLength - si.search_text_length;
            si.show_next = false;

            textArea.Select(si.current.Value, si.search_text_length);

            undos.Push(new UndoRedo(replaceTB.Text, si.current.Value, 3, textArea.SelectedText));

            textArea.SelectedText = replaceTB.Text;
            textArea.Select(si.current.Value, replaceTB.TextLength);
            textArea.Focus();

            LinkedListNode<int> curr = si.current.Next;

            while (curr != null)
            {
                curr.Value += offset;
                curr = curr.Next;
            }

            curr = si.current;

            if ((si.current = si.current.Next) == null)
                si.current = si.indices.First;

            si.indices.Remove(curr);
        }

        private void FindPrevBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (si.current == null)
            {
                MessageBox.Show("No data...", "Search result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (si.show_next)
            {
                if ((si.current = si.current.Previous) == null)
                    si.current = si.indices.Last;
            }
            else si.show_next = true;

            textArea.Select(si.current.Value, si.search_text_length);
            textArea.Focus();
        }

        private void FindNextBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (si.indices.Count == 0)
            {
                MessageBox.Show("No data...", "Search result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (si.show_next)
            {
                if ((si.current = si.current.Next) == null)
                    si.current = si.indices.First;
            }
            else si.show_next = true;

            textArea.Select(si.current.Value, si.search_text_length);
            textArea.Focus();
        }

        private void FindBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e != null && e.Button != MouseButtons.Left || searchTB.TextLength == 0) return;

            si.indices = SearchKMP((lorwecaseCHB.Checked ? textArea.Text.ToLower() : textArea.Text), (lorwecaseCHB.Checked ? searchTB.Text.ToLower() : searchTB.Text));
            si.search_text_length = searchTB.Text.Length;

            if (si.indices.Count == 0)
                MessageBox.Show("No data...", "Search result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                findPrevBtn.Enabled = findNextBtn.Enabled = true;

                si.current = si.indices.First;

                if (e != null || (Keys)sender == Keys.Enter)
                {
                    si.show_next = true;

                    textArea.Select(si.current.Value, si.search_text_length);
                    textArea.Focus();
                }
            }
        }

        private void Sform_mouse_down(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            swm.is_swindow_mouse_down = true;
            if (sw_first_time_load)
            {
                sw_first_time_load = false;
                swm.dx = Cursor.Position.X - this.Location.X - (this.Width - searchWindow.Width) / 2;
                swm.dy = Cursor.Position.Y - this.Location.Y - searchWindow.Height / 2;
            }
            else
            {
                swm.dx = Cursor.Position.X - searchWindow.Location.X;
                swm.dy = Cursor.Position.Y - searchWindow.Location.Y;
            }
            swm.mouse_x = Cursor.Position.X;
            swm.mouse_y = Cursor.Position.Y;
        }

        private void Sform_mouse_up(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            swm.is_swindow_mouse_down = false;

            if (!is_search_popup_window && Search_window_condition())
            {
                mainLayoutPanel.RowStyles[0].Height = 0F;
                mainLayoutPanel.RowStyles[2].Height += 75F;
                searchWindow.Opacity = 1.0F;
                MinMaxSearch_Click(null, null);
                out_of_sw_move_interval = false;
            }
        }

        private void TextAreaKeyPress(object sender, KeyPressEventArgs e)
        {
            if (textArea.SelectionLength != 0)
                deletion_text = textArea.SelectedText;

            key_press = e;
            if (key_press.KeyChar == (char)Keys.Enter)
                TextAreaTextChanged(null, null);
        }

        private void TextAreaTextChanged(object sender, EventArgs e)
        {
            if (text_changed == false)
            {
                text_changed = true;
                fileNameLabel.Text += "*";
            }
            if (new_file) new_file = false;

            if (!ctrlZ && !ctrlY && key_press != null)
                UndoRedoStateUpdate();
            else
                ctrlZ = ctrlY = false;

            counter.CountBFC(textArea.Text);
            counterLabel.Text = counter.GetInfo();
        }
        private void UndoRedoStateUpdate()
        {
            if (key_press.KeyChar == (char)Keys.Back || key_press.KeyChar == 0)
            {
                if (!DeletingTimer.Enabled)
                    undos.Push(new UndoRedo("", textArea.SelectionStart, 2, ""));

                DeletingTimer.Enabled = false;

                UndoRedo top = undos.Peek();
                undos.Pop();

                if (key_press.KeyChar == (char)Keys.Back) // backspace
                    undos.Push(new UndoRedo(deletion_text + top.data, textArea.SelectionStart, 2, "")); //deletion_text + top.replace
                else // Delete
                    undos.Push(new UndoRedo(top.data + deletion_text, textArea.SelectionStart, 2, "")); //top.replace + deletion_text                

                DeletingTimer.Enabled = true;
            }
            else
            {
                // writing
                if (DeletingTimer.Enabled)
                {
                    DeletingTimer.Enabled = false;
                    undos.Push(new UndoRedo("", (textArea.SelectionStart == 0 ? 0 : textArea.SelectionStart - 1), 1, ""));
                    redos.Clear();
                }

                if (textArea.TextLength - cursor_location_offset == textArea.SelectionStart)
                {
                    if (undos.Count == 0 || (undos.Peek().data != "" && (undos.Peek().data[0] != ' ' || undos.Peek().data[0] != '\t') && (key_press.KeyChar == (char)Keys.Space || key_press.KeyChar == (char)Keys.Enter || key_press.KeyChar == (char)Keys.Tab)))
                    {
                        if (deletion_text == "")
                            undos.Push(new UndoRedo("", (textArea.SelectionStart == 0 ? 0 : textArea.SelectionStart - 1), 1, ""));
                    }

                    if (deletion_text != "")
                        undos.Push(new UndoRedo("", (textArea.SelectionStart == 0 ? 0 : textArea.SelectionStart), 3, deletion_text));

                    UndoRedo undo = undos.Peek();
                    undos.Pop();
                    undo.data += (ctrlX ? "" : key_press.KeyChar.ToString());
                    undos.Push(undo);

                    if (key_press.KeyChar == (char)Keys.Tab || key_press.KeyChar == (char)Keys.Enter)
                    {
                        if (deletion_text == "")
                            undos.Push(new UndoRedo("", textArea.SelectionStart, 1, ""));
                        else
                            undos.Push(new UndoRedo("", textArea.SelectionStart, 3, deletion_text));
                    }
                }
                else
                {
                    if (deletion_text == "")
                        undos.Push(new UndoRedo((ctrlX ? "" : key_press.KeyChar.ToString()), (textArea.SelectionStart == 0 ? 0 : textArea.SelectionStart - 1), 1, ""));
                    else
                        undos.Push(new UndoRedo((ctrlX ? "" : key_press.KeyChar.ToString()), (textArea.SelectionStart == 0 ? 0 : textArea.SelectionStart - 1), 3, deletion_text)); //

                    cursor_location_offset = textArea.TextLength - textArea.SelectionStart;
                }
            }

            redos.Clear();
            deletion_text = "";
            key_press = null;
        }
        void TextAreaKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                CtrlZAction();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                CtrlYAction();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                key_press = new KeyPressEventArgs((e.KeyCode == Keys.Delete ? '\0' : '\b'));
                if (textArea.SelectionLength == 0)
                {
                    if (e.KeyCode == Keys.Delete && textArea.SelectionStart != textArea.TextLength)
                        deletion_text = textArea.Text[textArea.SelectionStart].ToString();
                    else if (e.KeyCode == Keys.Back && textArea.SelectionStart != 0)
                        deletion_text = textArea.Text[textArea.SelectionStart - 1].ToString();
                }
                else
                    deletion_text = textArea.SelectedText;
            }
            else if (e.Shift && e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.Shift && e.KeyCode == Keys.Insert)
            {
                PastToolStripMenuItemClick(null, null);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Insert)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                deletion_text = textArea.SelectedText;
            }
        }

        private void CtrlZAction()
        {
            while (undos.Count != 0 && undos.Peek().data == "" && undos.Peek().replace == "")
                undos.Pop(); // world here for better result
            if (undos.Count != 0)
            {
                ctrlZ = true;

                UndoRedo undo = undos.Peek();
                undos.Pop();

                if (undo.mode == 1)
                {
                    SendMessage(textArea.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
                    textArea.Select(undo.st_index, undo.data.Length);
                    textArea.SelectedText = "";
                    textArea.DeselectAll();
                    SendMessage(textArea.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero); // turnes off selection blue area animation                                                
                }
                else if (undo.mode == 2)
                {
                    textArea.Select(undo.st_index, 0);
                    textArea.SelectedText = undo.data;
                    textArea.Select(undo.st_index, undo.data.Length);
                    textArea.SelectionStart = undo.st_index + undo.data.Length;
                }
                else if (undo.mode == 3)
                {
                    textArea.Select(undo.st_index, undo.data.Length);
                    textArea.SelectedText = undo.replace;
                    textArea.Select(undo.st_index, undo.replace.Length);
                    textArea.SelectionStart = undo.st_index + undo.replace.Length;
                }
                while (undo.mode == 4)
                {
                    textArea.Select(undo.st_index, undo.data.Length);
                    textArea.SelectedText = undo.replace;
                    textArea.Select(undo.st_index, undo.replace.Length);
                    redos.Push(undo);

                    if (undos.Count != 0 && undos.Peek().mode == 4)
                    {
                        undo = undos.Peek();
                        undos.Pop();
                    }
                    else
                        undo.mode = 0;
                }

                textArea.Refresh();
                if (undo.mode != 0)
                    redos.Push(undo);
                else
                    textArea.DeselectAll();
            }
        }

        private void CtrlYAction()
        {
            while (redos.Count != 0 && redos.Peek().data == "" && redos.Peek().replace == "")
                redos.Pop(); // world here for better result
            if (redos.Count != 0)
            {
                ctrlY = true;

                UndoRedo redo = redos.Peek();
                redos.Pop();

                if (redo.mode == 1)
                {
                    textArea.Select(redo.st_index, 0);
                    textArea.SelectedText = redo.data;
                    textArea.Select(redo.st_index, redo.data.Length);
                    textArea.SelectionStart = redo.st_index + redo.data.Length;
                }
                else if (redo.mode == 2)
                {
                    textArea.Select(redo.st_index, redo.data.Length);
                    textArea.SelectedText = "";
                }
                else if (redo.mode == 3)
                {
                    textArea.Select(redo.st_index, redo.replace.Length);
                    textArea.SelectedText = redo.data;
                    textArea.Select(redo.st_index, redo.data.Length);
                    textArea.SelectionStart = redo.st_index + redo.data.Length;
                }
                while (redo.mode == 4)
                {
                    textArea.Select(redo.st_index, redo.replace.Length);
                    textArea.SelectedText = redo.data;
                    textArea.Select(redo.st_index, redo.data.Length);
                    undos.Push(redo);

                    if (redos.Count != 0 && redos.Peek().mode == 4)
                    {
                        redo = redos.Peek();
                        redos.Pop();
                    }
                    else
                        redo.mode = 0;
                }

                textArea.Refresh();
                if (redo.mode != 0)
                    undos.Push(redo);
                else
                    textArea.DeselectAll();
            }
        }
        private void SearchTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindBtn_MouseClick(Keys.Enter, null);
                e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
            }
            else if (e.KeyCode == Keys.Escape && is_search_replace_window_open)
            {
                OpenAndCloseSearchAndReplaceWindow();
                e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
            }
        }

        private void TitleMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!is_search_popup_window) MinMaxSearch_Click(null, null);
        }

        private void Sform_move(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            int cpx = Cursor.Position.X, cpy = Cursor.Position.Y;

            if (swm.is_swindow_mouse_down && !is_search_popup_window && Out_of_sw_interval_condition(cpx, cpy))
            {
                out_of_sw_move_interval = true;
                searchWindow.Location = new Point(cpx - swm.dx, cpy - swm.dy);
                toolStripStatusLabel1.Text = searchWindow.Location.ToString() + Cursor.Position.ToString();

                if (Search_window_condition())
                {
                    if (mainLayoutPanel.RowStyles[0].Height == 0F)
                    {
                        mainLayoutPanel.RowStyles[0].Height = 75F;
                        mainLayoutPanel.RowStyles[2].Height -= 75F;
                        searchWindow.Opacity = 0.65F;
                    }
                }
                else if (mainLayoutPanel.RowStyles[0].Height == 75F)
                {
                    mainLayoutPanel.RowStyles[0].Height = 0F;
                    mainLayoutPanel.RowStyles[2].Height += 75F;
                    searchWindow.Opacity = 1.0F;
                }
            }

            if (Out_of_sw_interval_condition(cpx, cpy) && swm.is_swindow_mouse_down && is_search_popup_window)
            {
                OpenAndCloseSearchAndReplaceWindow();
                is_search_popup_window = !is_search_popup_window;
                OpenAndCloseSearchAndReplaceWindow();
            }
        }

        private bool Search_window_condition()
        {
            return searchWindow.Location.Y > this.Location.Y + 5 && searchWindow.Location.Y < this.Location.Y + 100 && searchWindow.Location.X > this.Location.X + 10 && searchWindow.Location.X < this.Location.X + this.Width - searchWindow.Width;
        }

        private bool Out_of_sw_interval_condition(int cpx, int cpy)
        {
            return out_of_sw_move_interval || (cpx < swm.mouse_x - 35 || cpx > swm.mouse_x + 35) || (cpy < swm.mouse_y - 35 || cpy > swm.mouse_y + 35);
        }

        private void Sform_sizeeventhandler(object sender, EventArgs e)
        {
            if (!is_search_popup_window && is_search_replace_window_open)
            {
                if (WindowState == FormWindowState.Minimized && searchWindow.Visible)
                    searchWindow.Hide();
                else if (WindowState != FormWindowState.Minimized && !searchWindow.Visible)
                    searchWindow.Show(this);
            }
        }

        private void NewToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (text_changed)
            {
                DialogResult res = MessageBox.Show("File has been changed.. Do you want to save it?", "Unsaved file!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                    SaveToolStripMenuItemClick(null, null);
                else if (res == DialogResult.Cancel)
                    return;
            }

            filepath = "";
            textArea.Text = "";
            fileNameLabel.Text = "New text file";
            text_changed = false;
            Reset();
        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (text_changed)
            {
                DialogResult res = MessageBox.Show("File has been changed.. Do you want to save it?", "Unsaved file!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                    SaveToolStripMenuItemClick(null, null);
                else if (res == DialogResult.Cancel)
                    return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    textArea.Text = File.ReadAllText(filePath);
                    fileNameLabel.Text = openFileDialog.SafeFileName;
                    filepath = openFileDialog.FileName;
                    text_changed = false;
                    new_file = false;
                    Reset();
                }
            }
        }

        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (filepath != "")
            {
                File.WriteAllText(filepath, textArea.Text);
                if (text_changed == true)
                {
                    fileNameLabel.Text = fileNameLabel.Text.Substring(0, fileNameLabel.Text.Length - 1);
                    text_changed = false;
                    new_file = false;
                }
            }
            else
            {
                SaveFile();
            }
        }

        private void SaveFile()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.FileName = (fileNameLabel.Text[fileNameLabel.Text.Length - 1] == '*' ? fileNameLabel.Text.Substring(0, fileNameLabel.Text.Length - 1) : fileNameLabel.Text);

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    File.WriteAllText(filePath, textArea.Text);
                    filepath = filePath;

                    fileNameLabel.Text = "";
                    int last_slash = -1;
                    for (int i = 0; i < filepath.Length; ++i)
                    {
                        if (filepath[i] == '\\')
                            last_slash = i;
                    }
                    for (int i = last_slash + 1; i < filepath.Length; ++i)
                        fileNameLabel.Text += filepath[i];

                    new_file = false;
                    text_changed = false;
                }
            }
        }

        private void SForm_Load(object sender, EventArgs e)
        {
            searchWindow.Location = new Point(this.Location.X + (searchWindow.Width / 5), this.Location.Y + searchWindow.Height);
        }

        private void SearchWindowOpt(bool to_window)
        {
            foreach (Control c in searchPanel.Controls)
            {
                if (to_window)
                    c.Location = new Point(c.Location.X - 125, c.Location.Y);
                else
                    c.Location = new Point(c.Location.X + 125, c.Location.Y);
            }
        }

        private void OpenAndCloseSearchAndReplaceWindow()
        {
            if (is_search_replace_window_open)
            {
                is_search_replace_window_open = false;
                if (is_search_popup_window)
                {
                    mainLayoutPanel.RowStyles[0].Height -= 115F;
                    mainLayoutPanel.RowStyles[2].Height += 115F;
                    mainLayoutPanel.Controls.Remove(searchPanel);
                }
                else
                {
                    SearchWindowOpt(false);
                    searchWindow.Controls.Remove(searchPanel);
                    searchWindow.Hide();
                }
                this.ActiveControl = textArea;
            }
            else
            {
                is_search_replace_window_open = true;
                if (is_search_popup_window)
                {
                    mainLayoutPanel.RowStyles[0].Height += 115F;
                    mainLayoutPanel.RowStyles[2].Height -= 115F;
                    mainLayoutPanel.Controls.Add(searchPanel, 0, 0);
                    this.ActiveControl = searchTB;
                }
                else
                {
                    SearchWindowOpt(true);
                    searchWindow.Controls.Add(searchPanel);
                    searchWindow.Show(this);
                    searchWindow.ActiveControl = searchTB;
                }
            };
        }

        private void FindToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (!is_search_replace_window_open) OpenAndCloseSearchAndReplaceWindow();
            else searchTB.Focus();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            searchWindow.DesktopLocation = new Point(this.Location.X + (this.Width - searchWindow.Width) / 2, this.Location.Y + 50);
            this.MinimumSize = this.Size;
        }

        private void MinMaxSearch_Click(object sender, EventArgs e)
        {
            OpenAndCloseSearchAndReplaceWindow();
            is_search_popup_window = !is_search_popup_window;
            OpenAndCloseSearchAndReplaceWindow();
            sw_first_time_load = !sw_first_time_load;
        }

        private void CloseSearch_Click(object sender, EventArgs e)
        {
            OpenAndCloseSearchAndReplaceWindow();
        }

        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (new_file || !text_changed) return;

            DialogResult res = MessageBox.Show("File has been changed.. Do you want to save it?", "Exit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
                SaveToolStripMenuItemClick(null, null);
            else if (res == DialogResult.Cancel)
                e.Cancel = true;
        }

        private LinkedList<int> SearchKMP(string text, string pattern)
        {
            List<int> arr = SetupKMP(ref pattern);
            int frst = 0, sec = 0;
            LinkedList<int> searchs = new LinkedList<int>();

            while (frst < text.Length)
            {
                if (text[frst] == pattern[sec])
                {
                    frst++;
                    sec++;
                    if (sec == pattern.Length)
                    {
                        searchs.AddLast(frst - pattern.Length);
                        sec = 0;
                    }
                }
                else
                {
                    if (sec != 0) sec = arr[sec - 1];
                    else frst++;
                }
            }

            return searchs;
        }

        private List<int> SetupKMP(ref string s)
        {
            List<int> arr = new List<int>(new int[s.Length]);
            int frst = 0, sec = 1;

            while (sec < s.Length)
            {
                if (s[sec] == s[frst])
                {
                    arr[sec] = frst + 1;
                    sec++;
                    frst++;
                }
                else
                {
                    if (frst != 0) frst = arr[frst - 1];
                    else sec++;
                }
            }

            return arr;
        }

        private void CopyToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (textArea.SelectedText.Length == 0) return;
            Clipboard.SetText(textArea.SelectedText);
        }

        private void CutToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (textArea.SelectedText.Length == 0) return;

            Clipboard.SetText(textArea.SelectedText);
            deletion_text = textArea.SelectedText;

            ctrlX = true;
            key_press = new KeyPressEventArgs((char)Keys.A);
            textArea.SelectedText = "";
            ctrlX = false;
        }

        private void PastToolStripMenuItemClick(object sender, EventArgs e)
        {
            string s = Clipboard.GetText();

            if (DeletingTimer.Enabled == true)
                DeletingTimer_Tick(null, null);

            if (textArea.SelectionLength != 0)
                undos.Push(new UndoRedo(s, textArea.SelectionStart, 3, textArea.SelectedText));
            else
                undos.Push(new UndoRedo(s, textArea.SelectionStart, 1, ""));

            key_press = null;

            textArea.SelectedText = s;

            undos.Push(new UndoRedo("", textArea.SelectionStart - 1, 1, ""));
            redos.Clear();
        }

        private void DeletingTimer_Tick(object sender, EventArgs e)
        {
            DeletingTimer.Enabled = false;
            undos.Push(new UndoRedo("", textArea.SelectionStart, 1, ""));
            redos.Clear();
        }

        private void NewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
        }

        void SaveAsToolStripMenuItemClick(object sender, EventArgs e)
        {
            SaveFile();
        }

        void DefaultToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (defaultToolStripMenuItem.Checked == true) return;

            if (theme.name == "dark")
                darkToolStripMenuItem.Checked = false;
            else
                lightToolStripMenuItem.Checked = false;

            theme.name = "default";
            defaultToolStripMenuItem.Checked = true;

            ChangeTheme();
        }

        void LightToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (lightToolStripMenuItem.Checked == true) return;

            if (theme.name == "dark")
                darkToolStripMenuItem.Checked = false;
            else
                defaultToolStripMenuItem.Checked = false;

            theme.name = "light";
            lightToolStripMenuItem.Checked = true;

            ChangeTheme();
        }

        void DarkToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (darkToolStripMenuItem.Checked == true) return;

            if (theme.name == "light")
                lightToolStripMenuItem.Checked = false;
            else
                defaultToolStripMenuItem.Checked = false;

            theme.name = "dark";
            darkToolStripMenuItem.Checked = true;

            ChangeTheme();
        }

        void ChangeTheme()
        {
            if (theme.name == "default")
            {
                this.menuStrip1.BackColor = System.Drawing.Color.LightSlateGray;
                this.menuStrip1.ForeColor = System.Drawing.Color.Black;

                this.textArea.BackColor = System.Drawing.Color.FromArgb(22, 22, 22);
                this.textArea.ForeColor = System.Drawing.Color.AntiqueWhite;

                this.BackColor = System.Drawing.Color.SlateGray;
                this.ForeColor = Color.Black;
            }
            else if (theme.name == "dark")
            {
                this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(48, 31, 86);
                this.menuStrip1.ForeColor = System.Drawing.Color.White;

                this.textArea.BackColor = System.Drawing.Color.FromArgb(52, 50, 50);
                this.textArea.ForeColor = System.Drawing.Color.AntiqueWhite;

                this.BackColor = System.Drawing.Color.FromArgb(52, 50, 50);
                this.ForeColor = Color.White;
            }
            else
            {
                this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
                this.menuStrip1.ForeColor = System.Drawing.Color.Black;

                this.textArea.BackColor = System.Drawing.SystemColors.ControlDark;
                this.textArea.ForeColor = System.Drawing.Color.Black;

                this.BackColor = System.Drawing.SystemColors.Control;
                this.ForeColor = Color.Black;
            }
        }
    }

    public partial class NoCopyLabel : Label
    {
        private string text;

        public override string Text
        {
            get { return text; }
            set { if (value == null) value = text; if (text != value) { text = value; Refresh(); OnTextChanged(EventArgs.Empty); } }
        }
    }

    public class Counters
    {
        private int word_count, line_count, char_count;
        readonly private List<char> splits;
        readonly private List<char> special_splits;

        public Counters(string splits, string special_splits)
        {
            this.splits = new List<char>();
            this.special_splits = new List<char>();

            for (int i = 0; i < splits.Length; i++)
                this.splits.Add(splits[i]);

            for (int i = 0; i < special_splits.Length; i++)
                this.special_splits.Add(special_splits[i]);
        }

        public void CountBFC(string s)
        {
            bool add_to_chars = true;
            bool can_make_new_word = true;
            bool add_to_last_word = true;

            line_count = 1;
            char_count = 0;
            word_count = 0;

            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 0; j < special_splits.Count; j++)
                {
                    if (s[i] == special_splits[j])
                    {
                        add_to_chars = false;
                        break;
                    }
                }
                if (add_to_chars)
                    char_count++;
                add_to_chars = true;

                if (s[i] == '\n')
                    line_count++;

                for (int j = 0; j < splits.Count; j++)
                {
                    if (s[i] == splits[j])
                    {
                        can_make_new_word = true;
                        add_to_last_word = false;
                        break;
                    }
                    else
                    {
                        add_to_last_word = true;
                    }
                }

                if (can_make_new_word && add_to_last_word)
                {
                    word_count++;
                    can_make_new_word = false;
                }
            }
        }

        internal string GetInfo()
        {
            return "Characters: " + char_count + " | Words: " + word_count + " | Lines: " + line_count;
        }
    }
}