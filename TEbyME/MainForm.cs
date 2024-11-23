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

namespace TEbyME
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	
	public partial class MainForm : Form
	{
		// make window do not disapear
		private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
		private const UInt32 SWP_NOSIZE = 0x0001;
		private const UInt32 SWP_NOMOVE = 0x0002;
		private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
		
		[DllImport("user32.dll")] 
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
		//end
		
		string filepath;
		bool text_changed, is_search_replace_window_open, is_search_popup_indow;
		int screen_width;
		bool is_swindow_mouse_down;

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
			if (path != string.Empty){
				filepath = path;
				textArea.Text = File.ReadAllText(filepath);
				text_changed = false;
				
				fileNameLabel.Text = "";
                int last_slash = -1;
                for (int i = 0; i < filepath.Length; ++i){
                	if (filepath[i] == '\\')
                		last_slash = i;
                }
                for (int i = last_slash + 1; i < filepath.Length; ++i)
                	fileNameLabel.Text += filepath[i];
			}else{
				filepath = "";
				text_changed = true;
			}

            is_search_replace_window_open = false;
			is_search_popup_indow = true;
			is_swindow_mouse_down = false;			
						
			int hei = 113, wid = 663;
	        searchWindow = new Form()
			{
				Size = new Size(wid, hei),
				MaximumSize = new Size(wid, hei),
				MinimumSize = new Size(wid, hei),
                Font = new Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
				Name = "SearchWindow",
				Text = "S&R",
				ShowIcon = false,
				ControlBox = false,
				StartPosition = FormStartPosition.Manual,
				FormBorderStyle = FormBorderStyle.None,				
            };	        
	        
            SetWindowPos(searchWindow.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            this.SizeChanged += new EventHandler(sform_sizeeventhandler);  
           	searchWindow.Load += new EventHandler(sform_load);
           	
//           	searchWindow.Move += new EventHandler(sform_move);
//           	searchWindow.Click += new EventHandler(sform_mouse_down);
//           	searchWindow.MouseUp += new MouseEventHandler(sform_mouse_up);
        }
        
//        
        
        // dyn. beh
        void sform_mouse_down(object sender, EventArgs e){
//        	is_swindow_mouse_down = true;
			MessageBox.Show("Click");
        }
        
        void sform_mouse_up(object sender, EventArgs e){
//        	is_swindow_mouse_down = false;
        }
        
        void sform_move(object sender, EventArgs e){
        	if (!is_swindow_mouse_down)
        		toolStripStatusLabel1.Text = (Cursor.Position.X - this.Location.X) + " " + (Cursor.Position.Y - this.Location.Y);
        }// dyn. beh.
        
        void sform_load(object sedner, EventArgs e){
			//searchWindow.BackColor = Color.Red; //Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            searchWindow.Location = new Point(this.Location.X + (searchWindow.Width / 5), this.Location.Y + Convert.ToInt32((double)searchWindow.Height / 1.5));
        }
        
        void sform_sizeeventhandler(object sender, EventArgs e){
        	if (!is_search_popup_indow && is_search_replace_window_open){
        		if (WindowState == FormWindowState.Minimized)
        			searchWindow.Hide();
        		else
        			searchWindow.Show();
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
			if (filepath != ""){
				File.WriteAllText(filepath, textArea.Text);
				if (text_changed == true){
					fileNameLabel.Text = fileNameLabel.Text.Substring(0, fileNameLabel.Text.Length - 1);
				}
			}else{
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
	                    for (int i = 0; i < filepath.Length; ++i){
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

        private void MainForm_Load(object sender, EventArgs e)
        {            
			screen_width = this.Width;
        }
        
        void SearchWindowOpt(bool to_window){
        	foreach (Control c in searchPanel.Controls){
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
			}else if (e.Control && e.KeyCode == Keys.O){
				OpenToolStripMenuItemClick(null, null);
				e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
			}else if (e.Control && e.KeyCode == Keys.N){
				NewToolStripMenuItemClick(null, null);
				e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
			}else if (e.Control && e.KeyCode == Keys.F)
            {
                OpenAndCloseSearchAndReplaceWindow();
                e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
            }else if (e.Control && e.KeyCode == Keys.V){
				string pasttext = Clipboard.GetText();
				textArea.SelectedText = pasttext;
				e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
			}
            else
            {
				if (text_changed == false){
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
                if (is_search_popup_indow)
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
				if (is_search_popup_indow) 
				{
                    mainLayoutPanel.RowStyles[0].Height += 115F;
                    mainLayoutPanel.RowStyles[2].Height -= 115F;
                    mainLayoutPanel.Controls.Add(searchPanel, 0, 0);
                    this.ActiveControl = searchTB;
                }
				else
				{
					SearchWindowOpt(true);
                    searchWindow.Controls.Add (searchPanel);
					searchWindow.Show();
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

        List<int> SearchKMP(ref string text, ref string pattern){
            List<int> arr = SetupKMP(ref pattern);
			int frst = 0, sec = 0;
			List<int> searchs = new List<int>();
			
			while (frst < text.Length){
				if (text[frst] == pattern[sec]){
	                frst++;
	                sec++;
	                if (sec == pattern.Length){
	                	searchs.Add(frst - pattern.Length);
	                	sec = 0;
	                }
	            }else{
	                if (sec != 0) sec = arr[sec - 1];
	                else frst++;
	            }
			}
			
			return searchs;
		}
		
		List<int> SetupKMP(ref string s){
            List<int> arr = new List<int>(new int[s.Length]);
			int frst = 0, sec = 1;
			
			while (sec < s.Length){
				if (s[sec] == s[frst]){
	                arr[sec] = frst + 1;
	                sec++;
	                frst++;
	            }else{
	                if (frst != 0) frst = arr[frst - 1];
	                else sec++;
	            }
			}
			
			return arr;
		}
		
		void FindToolStripMenuItemClick(object sender, EventArgs e)
		{
			OpenAndCloseSearchAndReplaceWindow();
			return;

//            if (is_search_wind_open) return;
			
//			is_search_wind_open = true;
			Form search = new Form(){
				Size = new Size(950, 150),
                Font = new Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
				Name = "SearchWindow",
				ShowIcon = false
            };
			
//			search.Controls.Add(searchPanel);
			search.Closing += delegate (object sender_, CancelEventArgs e_) { Close_search(sender_, e_, ref textArea); };			
			
//			search.Show();
			
//			return;
			
			TextBox stb = new TextBox(){
				Size = new Size(300, 50),
				Location = new Point(25, 10),
				TabIndex = 0,
				Name = "STB"
			};

			Button sbt = new Button() {
				Size = new Size(130, 50),
				Location = new Point(10, stb.Location.Y + stb.Height + 15),				
				Text = "Search",
				TabIndex = 1,
				Name = "SBT"
			};

			Button nextf = new Button()
			{
				Size = new Size(70, 50),
				Location = new Point(sbt.Location.X + sbt.Width + 10, sbt.Location.Y),
				Text = "Search next",
				TabIndex = 2,
				Name = "SBTNext",
				Enabled = false,
                Font = new Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
            };

            Button prevf = new Button()
            {
                Size = new Size(70, 50),
                Location = new Point(nextf.Location.X + nextf.Width + 10, sbt.Location.Y),
                Text = "Search prev.",
                TabIndex = 3,
                Name = "SBTPrev",
				Enabled = false,
                Font = new Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
            };

            List<int> searchResult = new List<int>();
			int[] current_result_index = new int[2] { 0, 0 };

            sbt.Click += delegate (object sender_, EventArgs e_){ Search_btn_click_inside_search_window(sender_, e_, ref search, ref searchResult, ref current_result_index);};
            prevf.Click += delegate (object sender_, EventArgs e_) { Prev_search_btn_click(sender_, e_, searchResult, ref current_result_index); };
            nextf.Click += delegate (object sender_, EventArgs e_) { Next_search_btn_click(sender_, e_, searchResult, ref current_result_index); };

            search.Load += delegate (object sender_, EventArgs e_) { On_search_wind_load(sender_, e_, ref stb, ref search); };
            

            search.Controls.Add(stb);
			search.Controls.Add(sbt);
			search.Controls.Add(nextf);
			search.Controls.Add(prevf);
			
			SetWindowPos(search.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            search.StartPosition = FormStartPosition.Manual;
			
            search.Location = new Point((screen_width - search.Width) / 2, search.Location.Y);

            search.Show();
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

        void On_search_wind_load (object sender_, EventArgs e_, ref TextBox tb, ref Form f) {
			f.ActiveControl = tb;
		}

        private void MainForm_Resize(object sender, EventArgs e)
        {
			toolStripStatusLabel1.Text = Size.ToString();
        }

        private void minMaxSearch_Click(object sender, EventArgs e)
        {
            OpenAndCloseSearchAndReplaceWindow();
            if (is_search_popup_indow)
                is_search_popup_indow = false;
            else
                is_search_popup_indow = true;
            OpenAndCloseSearchAndReplaceWindow();
        }

        private void closeSearch_Click(object sender, EventArgs e)
        {
            OpenAndCloseSearchAndReplaceWindow();
        }

        void Close_search(object sender_, CancelEventArgs e_, ref RichTextBox rtb){
			//is_search_wind_open = false;
			OpenAndCloseSearchAndReplaceWindow();
        }		
		
		void Search_btn_click_inside_search_window(object sender, EventArgs e, ref Form f ,ref List<int> searchResult, ref int[] current_result_index)
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
				current_result_index = new int[2]{ 0, patt_.Length };

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
}