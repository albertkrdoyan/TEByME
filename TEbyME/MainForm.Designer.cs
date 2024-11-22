/*
 * Created by SharpDevelop.
 * User: User
 * Date: 04.11.2024
 * Time: 22:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Drawing;
using System.Windows.Forms;

namespace TEbyME
{
    partial class MainForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.status_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.title = new System.Windows.Forms.Label();
            this.minMaxSearch = new System.Windows.Forms.Label();
            this.closeSearch = new System.Windows.Forms.Label();
            this.searchTB = new System.Windows.Forms.TextBox();
            this.replaceTB = new System.Windows.Forms.TextBox();
            this.findBtn = new System.Windows.Forms.Button();
            this.findNextBtn = new System.Windows.Forms.Button();
            this.findPrevBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.replaceAllBtn = new System.Windows.Forms.Button();
            this.replaceBtn = new System.Windows.Forms.Button();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.textArea = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.mainLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // status_label
            // 
            this.status_label.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(23, 23);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(23, 23);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.findToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(55, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(275, 30);
            this.newToolStripMenuItem.Text = "New              ";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItemClick);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(275, 30);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(275, 30);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findToolStripMenuItem.Size = new System.Drawing.Size(275, 30);
            this.findToolStripMenuItem.Text = "Find";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.FindToolStripMenuItemClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightSlateGray;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.menuStrip1.ForeColor = System.Drawing.Color.Black;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1048, 33);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.Red;
            this.searchPanel.Controls.Add(this.title);
            this.searchPanel.Controls.Add(this.minMaxSearch);
            this.searchPanel.Controls.Add(this.closeSearch);
            this.searchPanel.Controls.Add(this.searchTB);
            this.searchPanel.Controls.Add(this.replaceTB);
            this.searchPanel.Controls.Add(this.findBtn);
            this.searchPanel.Controls.Add(this.findNextBtn);
            this.searchPanel.Controls.Add(this.findPrevBtn);
            this.searchPanel.Controls.Add(this.clearBtn);
            this.searchPanel.Controls.Add(this.replaceAllBtn);
            this.searchPanel.Controls.Add(this.replaceBtn);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchPanel.Location = new System.Drawing.Point(3, 3);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(1042, 104);
            this.searchPanel.TabIndex = 0;
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(256, 3);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(448, 25);
            this.title.TabIndex = 0;
            this.title.Text = "Search and Replace";
            this.title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // minMaxSearch
            // 
            this.minMaxSearch.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.minMaxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minMaxSearch.Location = new System.Drawing.Point(710, 3);
            this.minMaxSearch.Name = "minMaxSearch";
            this.minMaxSearch.Size = new System.Drawing.Size(25, 25);
            this.minMaxSearch.TabIndex = 0;
            this.minMaxSearch.Text = "[]";
            this.minMaxSearch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // closeSearch
            // 
            this.closeSearch.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.closeSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeSearch.Location = new System.Drawing.Point(744, 3);
            this.closeSearch.Name = "closeSearch";
            this.closeSearch.Size = new System.Drawing.Size(25, 25);
            this.closeSearch.TabIndex = 0;
            this.closeSearch.Text = "X";
            this.closeSearch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // searchTB
            // 
            this.searchTB.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.searchTB.Location = new System.Drawing.Point(256, 31);
            this.searchTB.Name = "searchTB";
            this.searchTB.Size = new System.Drawing.Size(255, 36);
            this.searchTB.TabIndex = 0;
            // 
            // replaceTB
            // 
            this.replaceTB.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.replaceTB.Location = new System.Drawing.Point(517, 31);
            this.replaceTB.Name = "replaceTB";
            this.replaceTB.Size = new System.Drawing.Size(255, 36);
            this.replaceTB.TabIndex = 0;
            // 
            // findBtn
            // 
            this.findBtn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.findBtn.Location = new System.Drawing.Point(256, 78);
            this.findBtn.Name = "findBtn";
            this.findBtn.Size = new System.Drawing.Size(75, 23);
            this.findBtn.TabIndex = 0;
            this.findBtn.UseVisualStyleBackColor = false;
            // 
            // findNextBtn
            // 
            this.findNextBtn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.findNextBtn.Location = new System.Drawing.Point(326, 78);
            this.findNextBtn.Name = "findNextBtn";
            this.findNextBtn.Size = new System.Drawing.Size(75, 23);
            this.findNextBtn.TabIndex = 0;
            this.findNextBtn.UseVisualStyleBackColor = false;
            // 
            // findPrevBtn
            // 
            this.findPrevBtn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.findPrevBtn.Location = new System.Drawing.Point(407, 78);
            this.findPrevBtn.Name = "findPrevBtn";
            this.findPrevBtn.Size = new System.Drawing.Size(75, 23);
            this.findPrevBtn.TabIndex = 0;
            this.findPrevBtn.UseVisualStyleBackColor = false;
            // 
            // clearBtn
            // 
            this.clearBtn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.clearBtn.Location = new System.Drawing.Point(488, 78);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(75, 23);
            this.clearBtn.TabIndex = 0;
            this.clearBtn.UseVisualStyleBackColor = false;
            // 
            // replaceAllBtn
            // 
            this.replaceAllBtn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.replaceAllBtn.Location = new System.Drawing.Point(694, 73);
            this.replaceAllBtn.Name = "replaceAllBtn";
            this.replaceAllBtn.Size = new System.Drawing.Size(75, 23);
            this.replaceAllBtn.TabIndex = 0;
            this.replaceAllBtn.UseVisualStyleBackColor = false;
            // 
            // replaceBtn
            // 
            this.replaceBtn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.replaceBtn.Location = new System.Drawing.Point(573, 78);
            this.replaceBtn.Name = "replaceBtn";
            this.replaceBtn.Size = new System.Drawing.Size(75, 23);
            this.replaceBtn.TabIndex = 0;
            this.replaceBtn.UseVisualStyleBackColor = false;
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileNameLabel.Location = new System.Drawing.Point(3, 110);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(1042, 30);
            this.fileNameLabel.TabIndex = 1;
            this.fileNameLabel.Text = "New*";
            this.fileNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textArea
            // 
            this.textArea.AcceptsTab = true;
            this.textArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.textArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textArea.ForeColor = System.Drawing.Color.AntiqueWhite;
            this.textArea.Location = new System.Drawing.Point(4, 145);
            this.textArea.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textArea.Name = "textArea";
            this.textArea.Size = new System.Drawing.Size(1040, 468);
            this.textArea.TabIndex = 0;
            this.textArea.Text = "";
            this.textArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextAreaKeyDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 651);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(1048, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mainLayoutPanel
            // 
            this.mainLayoutPanel.ColumnCount = 1;
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 590F));
            //this.mainLayoutPanel.Controls.Add(this.searchPanel, 0, 0);
            this.mainLayoutPanel.Controls.Add(this.fileNameLabel, 0, 1);
            this.mainLayoutPanel.Controls.Add(this.textArea, 0, 2);
            this.mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayoutPanel.Location = new System.Drawing.Point(0, 33);
            this.mainLayoutPanel.Name = "mainLayoutPanel";
            this.mainLayoutPanel.RowCount = 3;
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 450F));
            this.mainLayoutPanel.Size = new System.Drawing.Size(1048, 618);
            this.mainLayoutPanel.TabIndex = 15;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(1048, 673);
            this.Controls.Add(this.mainLayoutPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "TEbyME v1.0";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.mainLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.RichTextBox textArea;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private ToolStripStatusLabel status_label;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panel searchPanel;
        private Label minMaxSearch;
        private Button clearBtn;
        private Button findPrevBtn;
        private Button findBtn;
        private Button findNextBtn;
        private Button replaceAllBtn;
        private Button replaceBtn;
        private TextBox replaceTB;
        private TextBox searchTB;
        private Label closeSearch;
        private Label title;
        private StatusStrip statusStrip1;
        private TableLayoutPanel mainLayoutPanel;
    }
}