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

        bool ctrlz, ctrly;
        int cursor_location_offset;

        private struct Undo
        {
            public string data;
            public int st_index;

            public Undo(string d, int s)
            {
                data = d;
                st_index = s;
            }
        }
        Stack<Undo> undos;

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

        private void PathInit(string path)
        {
            if (path != string.Empty)
            {
                filepath = path;
                textArea.Text = File.ReadAllText(filepath);
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

            undos = new Stack<Undo>();
            ctrly = ctrlz = false;
            cursor_location_offset = 0;

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

            string newText = textArea.Text.Substring(0, si.indices.First.Value);

            for (LinkedListNode<int> curr = si.indices.First; curr != null; curr = curr.Next)
            {
                newText += replaceTB.Text + textArea.Text.Substring(curr.Value + si.search_text_length,
                    (curr.Next != null ? curr.Next.Value - curr.Value - si.search_text_length : textArea.TextLength - curr.Value - si.search_text_length));
            }

            si = new SearchIN();

            textArea.Text = newText;
        }

        private void ReplaceBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || replaceTB.TextLength == 0) return;

            if (si.current == null)
                FindBtn_MouseClick(Keys.Enter, null);

            if (si.current == null)
                return;

            int offset = replaceTB.TextLength - si.search_text_length;
            si.show_next = false;

            textArea.Text = textArea.Text.Substring(0, si.current.Value)
                + replaceTB.Text
                + textArea.Text.Substring(si.current.Value + si.search_text_length, textArea.TextLength - si.current.Value - si.search_text_length);

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
            key_press = e;
            if (key_press.KeyChar == '\b' || key_press.KeyChar == (char)Keys.Enter)
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

            if (ctrlz || ctrly || key_press == null)
            {
                ctrlz = ctrly = false;
                return;
            }

            if (key_press.KeyChar == (char)Keys.Back || key_press.KeyChar == 0)
            {
            	if (key_press.KeyChar == (char)Keys.Back)
            		toolStripStatusLabel1.Text = "Back";
            	else if (key_press.KeyChar == 0)
            		toolStripStatusLabel1.Text = "Del";
            }
            else{
            	if (textArea.TextLength - cursor_location_offset == textArea.SelectionStart)
	            {
					if (undos.Count == 0 || (undos.Peek().data != "" && (undos.Peek().data[0] != ' ' || undos.Peek().data[0] != '\t') && (key_press.KeyChar == (char)Keys.Space || key_press.KeyChar == (char)Keys.Enter || key_press.KeyChar == (char)Keys.Tab)))
	                    undos.Push(new Undo("", textArea.SelectionStart - 1));
	                
	                Undo undo = undos.Peek();
	                undos.Pop();
	                undo.data += key_press.KeyChar.ToString();
	                undos.Push(undo);
	            }
	            else
	            {
	                undos.Push(new Undo(key_press.KeyChar.ToString(), textArea.SelectionStart - 1));
	                cursor_location_offset = textArea.TextLength - textArea.SelectionStart;
	            }
	            
	            toolStripStatusLabel1.Text = "{" + Convert.ToInt32((char)key_press.KeyChar).ToString() + " : " + key_press.KeyChar.ToString() + "}";
            }
            
            toolStripStatusLabel1.Text += ", text len: " + textArea.TextLength + ", last index: " + textArea.SelectionStart.ToString();

            key_press = null;
        }

        void TextAreaKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                while (undos.Count != 0 && undos.Peek().data == "")
                    undos.Pop();
                if (undos.Count != 0)
                {
                    ctrlz = true;
                    Undo undo = undos.Peek();
                    undos.Pop();

                    SendMessage(textArea.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
                    textArea.Select(undo.st_index, undo.data.Length);
                    textArea.SelectedText = "";
                    textArea.DeselectAll();
                    SendMessage(textArea.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
                    textArea.Refresh(); // turnes off selection blue area animation

                    toolStripStatusLabel1.Text = "Ctrl + Z";
                }
                e.SuppressKeyPress = true;
            }
            else if(e.KeyCode == Keys.Delete)
            	key_press = new KeyPressEventArgs('\0');
            else if (e.Shift && e.KeyCode == Keys.Delete){
            	e.SuppressKeyPress = true;
            }else if (e.Shift && e.KeyCode == Keys.Insert){
            	PastToolStripMenuItemClick(null, null);
            	e.SuppressKeyPress = true;
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
                if (WindowState == FormWindowState.Minimized)
                    searchWindow.Hide();
                else
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
            textArea.SelectedText = "";
        }

        private void PastToolStripMenuItemClick(object sender, EventArgs e)
        {
            string s = Clipboard.GetText();

            undos.Push(new Undo(s, textArea.SelectionStart));

            textArea.SelectedText = s;

            undos.Push(new Undo("", textArea.SelectionStart));
            
            toolStripStatusLabel1.Text = "Past";
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
}
//Несоответствие между архитектурой процессора проекта "MSIL", построение которого выполняется, и архитектурой процессора ссылки "C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll", "x86". Это несоответствие может привести к ошибкам во время выполнения. Попробуйте изменить целевую архитектуру процессора для проекта с помощью диспетчера конфигураций, чтобы согласовать архитектуры процессоров для проекта и ссылок, или используйте зависимость от ссылок с архитектурой процессора, соответствующей целевой архитектуре процессора проекта. (MSB3270)
//Несоответствие между архитектурой процессора проекта "MSIL", построение которого выполняется, и архитектурой процессора ссылки "System.Data", "AMD64". Это несоответствие может привести к ошибкам во время выполнения. Попробуйте изменить целевую архитектуру процессора для проекта с помощью диспетчера конфигураций, чтобы согласовать архитектуры процессоров для проекта и ссылок, или используйте зависимость от ссылок с архитектурой процессора, соответствующей целевой архитектуре процессора проекта. (MSB3270)