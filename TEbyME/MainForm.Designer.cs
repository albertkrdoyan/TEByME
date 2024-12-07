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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.status_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.lorwecaseCHB = new System.Windows.Forms.CheckBox();
            this.title = new TEbyME.NoCopyLabel();
            this.minMaxSearch = new TEbyME.NoCopyLabel();
            this.closeSearch = new TEbyME.NoCopyLabel();
            this.searchTB = new System.Windows.Forms.TextBox();
            this.replaceTB = new System.Windows.Forms.TextBox();
            this.findBtn = new System.Windows.Forms.Button();
            this.findNextBtn = new System.Windows.Forms.Button();
            this.findPrevBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.replaceAllBtn = new System.Windows.Forms.Button();
            this.replaceBtn = new System.Windows.Forms.Button();
            this.textArea = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.fileNameLabel = new TEbyME.NoCopyLabel();
            this.menuStrip1.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 16);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.newWindowToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.findToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(55, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::TEbyME.Resource1.new_file;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(331, 30);
            this.newToolStripMenuItem.Text = "New              ";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItemClick);
            // 
            // newWindowToolStripMenuItem
            // 
            this.newWindowToolStripMenuItem.Image = global::TEbyME.Resource1.new_window1;
            this.newWindowToolStripMenuItem.Name = "newWindowToolStripMenuItem";
            this.newWindowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.newWindowToolStripMenuItem.Size = new System.Drawing.Size(331, 30);
            this.newWindowToolStripMenuItem.Text = "New Window";
            this.newWindowToolStripMenuItem.Click += new System.EventHandler(this.NewWindowToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::TEbyME.Resource1.open_file;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(331, 30);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::TEbyME.Resource1.save_file;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(331, 30);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = global::TEbyME.Resource1.save_file_as;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(331, 30);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(328, 6);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Image = global::TEbyME.Resource1.find;
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findToolStripMenuItem.Size = new System.Drawing.Size(331, 30);
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
            this.fileToolStripMenuItem,
            this.windowToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(920, 33);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themesToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(96, 29);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // themesToolStripMenuItem
            // 
            this.themesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultToolStripMenuItem,
            this.lightToolStripMenuItem,
            this.darkToolStripMenuItem});
            this.themesToolStripMenuItem.Name = "themesToolStripMenuItem";
            this.themesToolStripMenuItem.Size = new System.Drawing.Size(163, 30);
            this.themesToolStripMenuItem.Text = "Themes";
            // 
            // defaultToolStripMenuItem
            // 
            this.defaultToolStripMenuItem.Checked = true;
            this.defaultToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.defaultToolStripMenuItem.Name = "defaultToolStripMenuItem";
            this.defaultToolStripMenuItem.Size = new System.Drawing.Size(159, 30);
            this.defaultToolStripMenuItem.Text = "Default";
            this.defaultToolStripMenuItem.Click += new System.EventHandler(this.DefaultToolStripMenuItemClick);
            // 
            // lightToolStripMenuItem
            // 
            this.lightToolStripMenuItem.Name = "lightToolStripMenuItem";
            this.lightToolStripMenuItem.Size = new System.Drawing.Size(159, 30);
            this.lightToolStripMenuItem.Text = "Light";
            this.lightToolStripMenuItem.Click += new System.EventHandler(this.LightToolStripMenuItemClick);
            // 
            // darkToolStripMenuItem
            // 
            this.darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            this.darkToolStripMenuItem.Size = new System.Drawing.Size(159, 30);
            this.darkToolStripMenuItem.Text = "Dark";
            this.darkToolStripMenuItem.Click += new System.EventHandler(this.DarkToolStripMenuItemClick);
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.searchPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.searchPanel.Controls.Add(this.lorwecaseCHB);
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
            this.searchPanel.Location = new System.Drawing.Point(4, 4);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(928, 194);
            this.searchPanel.TabIndex = 0;
            // 
            // lorwecaseCHB
            // 
            this.lorwecaseCHB.AutoSize = true;
            this.lorwecaseCHB.BackColor = System.Drawing.Color.GhostWhite;
            this.lorwecaseCHB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lorwecaseCHB.Location = new System.Drawing.Point(140, 32);
            this.lorwecaseCHB.Name = "lorwecaseCHB";
            this.lorwecaseCHB.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lorwecaseCHB.Size = new System.Drawing.Size(70, 21);
            this.lorwecaseCHB.TabIndex = 2;
            this.lorwecaseCHB.Text = "Lower  ";
            this.lorwecaseCHB.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lorwecaseCHB.UseCompatibleTextRendering = true;
            this.lorwecaseCHB.UseVisualStyleBackColor = false;
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.GhostWhite;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.title.Location = new System.Drawing.Point(140, 3);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(567, 25);
            this.title.TabIndex = 0;
            this.title.Text = "Search and Replace";
            this.title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.title.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TitleMouseDoubleClick);
            // 
            // minMaxSearch
            // 
            this.minMaxSearch.BackColor = System.Drawing.Color.GhostWhite;
            this.minMaxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minMaxSearch.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.minMaxSearch.Location = new System.Drawing.Point(713, 3);
            this.minMaxSearch.Name = "minMaxSearch";
            this.minMaxSearch.Size = new System.Drawing.Size(25, 25);
            this.minMaxSearch.TabIndex = 0;
            this.minMaxSearch.Text = "⬜";
            this.minMaxSearch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // closeSearch
            // 
            this.closeSearch.BackColor = System.Drawing.Color.GhostWhite;
            this.closeSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeSearch.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.closeSearch.Location = new System.Drawing.Point(744, 3);
            this.closeSearch.Name = "closeSearch";
            this.closeSearch.Size = new System.Drawing.Size(25, 25);
            this.closeSearch.TabIndex = 0;
            this.closeSearch.Text = "X";
            this.closeSearch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // searchTB
            // 
            this.searchTB.BackColor = System.Drawing.Color.GhostWhite;
            this.searchTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchTB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.searchTB.Location = new System.Drawing.Point(246, 32);
            this.searchTB.Name = "searchTB";
            this.searchTB.Size = new System.Drawing.Size(266, 34);
            this.searchTB.TabIndex = 0;
            this.searchTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchTB_KeyDown);
            // 
            // replaceTB
            // 
            this.replaceTB.BackColor = System.Drawing.Color.GhostWhite;
            this.replaceTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.replaceTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replaceTB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.replaceTB.Location = new System.Drawing.Point(518, 32);
            this.replaceTB.Name = "replaceTB";
            this.replaceTB.Size = new System.Drawing.Size(252, 34);
            this.replaceTB.TabIndex = 5;
            // 
            // findBtn
            // 
            this.findBtn.BackColor = System.Drawing.Color.GhostWhite;
            this.findBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.findBtn.Location = new System.Drawing.Point(140, 68);
            this.findBtn.Name = "findBtn";
            this.findBtn.Size = new System.Drawing.Size(75, 35);
            this.findBtn.TabIndex = 2;
            this.findBtn.Text = "Find";
            this.findBtn.UseVisualStyleBackColor = false;
            // 
            // findNextBtn
            // 
            this.findNextBtn.BackColor = System.Drawing.Color.GhostWhite;
            this.findNextBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findNextBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.findNextBtn.Location = new System.Drawing.Point(219, 68);
            this.findNextBtn.Name = "findNextBtn";
            this.findNextBtn.Size = new System.Drawing.Size(75, 35);
            this.findNextBtn.TabIndex = 3;
            this.findNextBtn.Text = "Next";
            this.findNextBtn.UseVisualStyleBackColor = false;
            // 
            // findPrevBtn
            // 
            this.findPrevBtn.BackColor = System.Drawing.Color.GhostWhite;
            this.findPrevBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findPrevBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.findPrevBtn.Location = new System.Drawing.Point(300, 68);
            this.findPrevBtn.Name = "findPrevBtn";
            this.findPrevBtn.Size = new System.Drawing.Size(105, 35);
            this.findPrevBtn.TabIndex = 4;
            this.findPrevBtn.Text = "Previous";
            this.findPrevBtn.UseVisualStyleBackColor = false;
            // 
            // clearBtn
            // 
            this.clearBtn.BackColor = System.Drawing.Color.GhostWhite;
            this.clearBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.clearBtn.Location = new System.Drawing.Point(424, 68);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(75, 35);
            this.clearBtn.TabIndex = 8;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = false;
            // 
            // replaceAllBtn
            // 
            this.replaceAllBtn.BackColor = System.Drawing.Color.GhostWhite;
            this.replaceAllBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replaceAllBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.replaceAllBtn.Location = new System.Drawing.Point(630, 68);
            this.replaceAllBtn.Name = "replaceAllBtn";
            this.replaceAllBtn.Size = new System.Drawing.Size(139, 35);
            this.replaceAllBtn.TabIndex = 7;
            this.replaceAllBtn.Text = "Replace All";
            this.replaceAllBtn.UseVisualStyleBackColor = false;
            // 
            // replaceBtn
            // 
            this.replaceBtn.BackColor = System.Drawing.Color.GhostWhite;
            this.replaceBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replaceBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.replaceBtn.Location = new System.Drawing.Point(518, 68);
            this.replaceBtn.Name = "replaceBtn";
            this.replaceBtn.Size = new System.Drawing.Size(109, 35);
            this.replaceBtn.TabIndex = 6;
            this.replaceBtn.Text = "Replace";
            this.replaceBtn.UseVisualStyleBackColor = false;
            // 
            // textArea
            // 
            this.textArea.AcceptsTab = true;
            this.textArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.textArea.ContextMenuStrip = this.contextMenuStrip1;
            this.textArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textArea.ForeColor = System.Drawing.Color.AntiqueWhite;
            this.textArea.Location = new System.Drawing.Point(4, 34);
            this.textArea.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textArea.Name = "textArea";
            this.textArea.Size = new System.Drawing.Size(932, 509);
            this.textArea.TabIndex = 0;
            this.textArea.Text = "";
            this.textArea.TextChanged += new System.EventHandler(this.TextAreaTextChanged);
            this.textArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextAreaKeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.pastToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(168, 82);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::TEbyME.Resource1.copy;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItemClick);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = global::TEbyME.Resource1.cut;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItemClick);
            // 
            // pastToolStripMenuItem
            // 
            this.pastToolStripMenuItem.Image = global::TEbyME.Resource1.paste;
            this.pastToolStripMenuItem.Name = "pastToolStripMenuItem";
            this.pastToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pastToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.pastToolStripMenuItem.Text = "Past";
            this.pastToolStripMenuItem.Click += new System.EventHandler(this.PastToolStripMenuItemClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 581);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(920, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mainLayoutPanel
            // 
            this.mainLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.mainLayoutPanel.ColumnCount = 1;
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 938F));
            this.mainLayoutPanel.Controls.Add(this.fileNameLabel, 0, 1);
            this.mainLayoutPanel.Controls.Add(this.textArea, 0, 2);
            this.mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayoutPanel.Location = new System.Drawing.Point(0, 33);
            this.mainLayoutPanel.Name = "mainLayoutPanel";
            this.mainLayoutPanel.RowCount = 3;
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 403F));
            this.mainLayoutPanel.Size = new System.Drawing.Size(920, 548);
            this.mainLayoutPanel.TabIndex = 15;
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileNameLabel.Location = new System.Drawing.Point(4, 2);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(932, 27);
            this.fileNameLabel.TabIndex = 1;
            this.fileNameLabel.Text = "New text file";
            this.fileNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(920, 603);
            this.Controls.Add(this.mainLayoutPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Text Editor by M.E. | V2.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.Sform_sizeeventhandler);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.mainLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.ToolStripMenuItem darkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.RichTextBox textArea;
        private NoCopyLabel fileNameLabel;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private ToolStripStatusLabel status_label;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panel searchPanel;
        private NoCopyLabel minMaxSearch;
        private Button clearBtn;
        private Button findPrevBtn;
        private Button findBtn;
        private Button findNextBtn;
        private Button replaceAllBtn;
        private Button replaceBtn;
        private TextBox replaceTB;
        private TextBox searchTB;
        private NoCopyLabel closeSearch;
        private NoCopyLabel title;
        private StatusStrip statusStrip1;
        private TableLayoutPanel mainLayoutPanel;
        private CheckBox lorwecaseCHB;
        private ToolStripMenuItem newWindowToolStripMenuItem;
    }
}