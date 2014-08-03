namespace Sca.Tools.CodeAnalysis.Visualizer
{
    partial class CodeVisualizer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dependenciesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toDosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.centerPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1118, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dependenciesToolStripMenuItem,
            this.detailsToolStripMenuItem,
            this.toDosToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.optionsToolStripMenuItem.Text = "View";
            // 
            // dependenciesToolStripMenuItem
            // 
            this.dependenciesToolStripMenuItem.Name = "dependenciesToolStripMenuItem";
            this.dependenciesToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.dependenciesToolStripMenuItem.Text = "Dependencies";
            this.dependenciesToolStripMenuItem.Click += new System.EventHandler(this.dependenciesToolStripMenuItem_Click);
            // 
            // detailsToolStripMenuItem
            // 
            this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            this.detailsToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.detailsToolStripMenuItem.Text = "Details";
            this.detailsToolStripMenuItem.Click += new System.EventHandler(this.detailsToolStripMenuItem_Click);
            // 
            // toDosToolStripMenuItem
            // 
            this.toDosToolStripMenuItem.Name = "toDosToolStripMenuItem";
            this.toDosToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.toDosToolStripMenuItem.Text = "To-do";
            this.toDosToolStripMenuItem.Click += new System.EventHandler(this.toDosToolStripMenuItem_Click);
            // 
            // leftPanel
            // 
            this.leftPanel.Location = new System.Drawing.Point(12, 27);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(400, 600);
            this.leftPanel.TabIndex = 1;
            // 
            // centerPanel
            // 
            this.centerPanel.Location = new System.Drawing.Point(418, 27);
            this.centerPanel.Name = "centerPanel";
            this.centerPanel.Size = new System.Drawing.Size(688, 600);
            this.centerPanel.TabIndex = 2;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(477, 633);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(629, 43);
            this.bottomPanel.TabIndex = 3;
            // 
            // CodeVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 687);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.centerPanel);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CodeVisualizer";
            this.Text = "Code Visualizer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Panel centerPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.ToolStripMenuItem dependenciesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toDosToolStripMenuItem;
    }
}