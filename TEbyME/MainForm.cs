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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Security.Cryptography;

namespace TEbyME
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>

    public partial class MainForm : Form
    {
        string filepath;
        bool text_changed, is_search_replace_window_open, is_search_popup_window, sw_first_time_load, out_of_sw_move_interval;

        struct SWindowMove
        {
            public bool is_swindow_mouse_down;
            public int dx, dy;
            public int mouse_x, mouse_y;
        }

        SWindowMove swm;

        private Form searchWindow;

        public MainForm(string path)
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
            pathInit(path);

            is_search_replace_window_open = swm.is_swindow_mouse_down = out_of_sw_move_interval = false;
            sw_first_time_load = is_search_popup_window = true;

            swm = new MainForm.SWindowMove();

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

            this.closeSearch.MouseClick += closeSearch_Click;
            this.minMaxSearch.MouseClick += minMaxSearch_Click;

            this.title.MouseDown += sform_mouse_down;
            this.title.MouseUp += sform_mouse_up;
            this.title.MouseMove += sform_move;
        }

        void pathInit(string path)
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
                text_changed = true;
            }
        }

        void sform_mouse_down(object sender, MouseEventArgs e)
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

        void sform_mouse_up(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            swm.is_swindow_mouse_down = false;

            if (!is_search_popup_window && search_window_condition())
            {
                mainLayoutPanel.RowStyles[0].Height = 0F;
                mainLayoutPanel.RowStyles[2].Height += 75F;
                searchWindow.Opacity = 1.0F;
                minMaxSearch_Click(null, null);
                out_of_sw_move_interval = false;
            }
        }

        void titleMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!is_search_popup_window) minMaxSearch_Click(null, null);
        }

        void sform_move(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            int cpx = Cursor.Position.X, cpy = Cursor.Position.Y;

            if (swm.is_swindow_mouse_down && !is_search_popup_window && out_of_sw_interval_condition(cpx, cpy))
            {
                out_of_sw_move_interval = true;
                searchWindow.Location = new Point(cpx - swm.dx, cpy - swm.dy);
                toolStripStatusLabel1.Text = searchWindow.Location.ToString() + Cursor.Position.ToString();

                if (search_window_condition())
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

            if (out_of_sw_interval_condition(cpx, cpy) && swm.is_swindow_mouse_down && is_search_popup_window)
            {
                OpenAndCloseSearchAndReplaceWindow();
                is_search_popup_window = !is_search_popup_window;
                OpenAndCloseSearchAndReplaceWindow();
            }
        }

        private bool search_window_condition()
        {
            return searchWindow.Location.Y > this.Location.Y + 5 && searchWindow.Location.Y < this.Location.Y + 100 && searchWindow.Location.X > this.Location.X + 10 && searchWindow.Location.X < this.Location.X + this.Width - searchWindow.Width;
        }

        private bool out_of_sw_interval_condition(int cpx, int cpy)
        {
            return out_of_sw_move_interval || (cpx < swm.mouse_x - 35 || cpx > swm.mouse_x + 35) || (cpy < swm.mouse_y - 35 || cpy > swm.mouse_y + 35);
        }

        void sform_sizeeventhandler(object sender, EventArgs e)
        {
            if (!is_search_popup_window && is_search_replace_window_open)
            {
                if (WindowState == FormWindowState.Minimized)
                    searchWindow.Hide();
                else
                    searchWindow.Show(this);
            }
        }

        void NewToolStripMenuItemClick(object sender, EventArgs e)
        {
            filepath = "";
            textArea.Text = "";
            fileNameLabel.Text = "New*";
        }

        void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                text_changed = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    textArea.Text = File.ReadAllText(filePath);
                    fileNameLabel.Text = openFileDialog.SafeFileName;
                    filepath = openFileDialog.FileName;
                }
            }
        }

        void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (filepath != "")
            {
                File.WriteAllText(filepath, textArea.Text);
                if (text_changed == true)
                {
                    fileNameLabel.Text = fileNameLabel.Text.Substring(0, fileNameLabel.Text.Length - 1);
                }
            }
            else
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog.FilterIndex = 1;

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
                    }
                }
            }
            text_changed = false;
        }

        private void SForm_Load(object sender, EventArgs e)
        {
            searchWindow.Location = new Point(this.Location.X + (searchWindow.Width / 5), this.Location.Y + searchWindow.Height);
        }

        void SearchWindowOpt(bool to_window)
        {
            foreach (Control c in searchPanel.Controls)
            {
                if (to_window)
                    c.Location = new Point(c.Location.X - 125, c.Location.Y);
                else
                    c.Location = new Point(c.Location.X + 125, c.Location.Y);
            }
        }

        void TextAreaKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                SaveToolStripMenuItemClick(null, null);
                e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                OpenToolStripMenuItemClick(null, null);
                e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                NewToolStripMenuItemClick(null, null);
                e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                OpenAndCloseSearchAndReplaceWindow();
                e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                string pasttext = Clipboard.GetText();
                textArea.SelectedText = pasttext;
                e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
            }
            else
            {
                if (text_changed == false)
                {
                    text_changed = true;
                    fileNameLabel.Text += "*";
                }
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

        void searchTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                OpenAndCloseSearchAndReplaceWindow();
                e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
            }
        }

        List<int> SearchKMP(ref string text, ref string pattern)
        {
            List<int> arr = SetupKMP(ref pattern);
            int frst = 0, sec = 0;
            List<int> searchs = new List<int>();

            while (frst < text.Length)
            {
                if (text[frst] == pattern[sec])
                {
                    frst++;
                    sec++;
                    if (sec == pattern.Length)
                    {
                        searchs.Add(frst - pattern.Length);
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

        List<int> SetupKMP(ref string s)
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

        void FindToolStripMenuItemClick(object sender, EventArgs e)
        {
            OpenAndCloseSearchAndReplaceWindow();
        }

        private void Next_search_btn_click(object sender_, EventArgs e_, List<int> searchResult, ref int[] current_result_index)
        {
            if (++current_result_index[0] == searchResult.Count)
                current_result_index[0] = 0;

            textArea.Select(searchResult[current_result_index[0]], current_result_index[1]);
            textArea.Select(searchResult[current_result_index[0]], current_result_index[1]);
            textArea.Focus();
        }

        private void Prev_search_btn_click(object sender_, EventArgs e_, List<int> searchResult, ref int[] current_result_index)
        {
            if (--current_result_index[0] == -1)
                current_result_index[0] = searchResult.Count - 1;

            textArea.Select(searchResult[current_result_index[0]], current_result_index[1]);
            textArea.Select(searchResult[current_result_index[0]], current_result_index[1]);
            textArea.Focus();
        }

        void On_search_wind_load(object sender_, EventArgs e_, ref TextBox tb, ref Form f)
        {
            f.ActiveControl = tb;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            searchWindow.DesktopLocation = new Point(this.Location.X + (this.Width - searchWindow.Width) / 2, this.Location.Y + 50);
            this.MinimumSize = this.Size;
        }

        private void minMaxSearch_Click(object sender, EventArgs e)
        {
            OpenAndCloseSearchAndReplaceWindow();
            is_search_popup_window = !is_search_popup_window;
            OpenAndCloseSearchAndReplaceWindow();
            sw_first_time_load = !sw_first_time_load;
        }

        private void closeSearch_Click(object sender, EventArgs e)
        {
            OpenAndCloseSearchAndReplaceWindow();
        }

        void Search_btn_click_inside_search_window(object sender, EventArgs e, ref Form f, ref List<int> searchResult, ref int[] current_result_index)
        {
            Button sbt = (Button)f.Controls.Find("SBT", true)[0];
            TextBox patt = (TextBox)f.Controls.Find("STB", true)[0];
            sbt.Text = "New Search";

            string text_ = textArea.Text, patt_ = patt.Text;
            searchResult = SearchKMP(ref text_, ref patt_);
            Button[] btns = new Button[2] { (Button)f.Controls.Find("SBTNext", true)[0], (Button)f.Controls.Find("SBTPrev", true)[0] };

            if (searchResult.Count != 0)
            {
                btns[0].Enabled = btns[1].Enabled = true;
                current_result_index = new int[2] { 0, patt_.Length };

                textArea.Select(searchResult[current_result_index[0]], current_result_index[1]);
                textArea.Focus();
            }
            else
            {
                btns[0].Enabled = btns[1].Enabled = false;
                MessageBox.Show("No data...", "Search result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    public partial class PLabel : Label
    {
        private string text;

        public override string Text
        {
            get { return text; }
            set { if (value == null) value = text; if (text != value) { text = value; Refresh(); OnTextChanged(EventArgs.Empty); } }
        }
    }
}