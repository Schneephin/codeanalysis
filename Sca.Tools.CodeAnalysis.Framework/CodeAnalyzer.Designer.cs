namespace Sca.Tools.CodeAnalysis.Framework
{
    partial class CodeAnalyzer
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
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sprachpluginsHinzufügenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_path = new System.Windows.Forms.Label();
            this.lbl_language = new System.Windows.Forms.Label();
            this.lbl_sources = new System.Windows.Forms.Label();
            this.lbl_preview = new System.Windows.Forms.Label();
            this.tb_path = new System.Windows.Forms.TextBox();
            this.btn_path = new System.Windows.Forms.Button();
            this.cb_language = new System.Windows.Forms.ComboBox();
            this.lb_sourcefiles = new System.Windows.Forms.ListBox();
            this.tv_preview = new System.Windows.Forms.TreeView();
            this.btn_startAnalyze = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.einstellungenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(606, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.speichernToolStripMenuItem,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // speichernToolStripMenuItem
            // 
            this.speichernToolStripMenuItem.Name = "speichernToolStripMenuItem";
            this.speichernToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.speichernToolStripMenuItem.Text = "Speichern";
            this.speichernToolStripMenuItem.Click += new System.EventHandler(this.speichernToolStripMenuItem_Click);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.beendenToolStripMenuItem.Text = "Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sprachpluginsHinzufügenToolStripMenuItem});
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.einstellungenToolStripMenuItem.Text = "Einstellungen";
            // 
            // sprachpluginsHinzufügenToolStripMenuItem
            // 
            this.sprachpluginsHinzufügenToolStripMenuItem.Name = "sprachpluginsHinzufügenToolStripMenuItem";
            this.sprachpluginsHinzufügenToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.sprachpluginsHinzufügenToolStripMenuItem.Text = "Sprachplugins hinzufügen";
            this.sprachpluginsHinzufügenToolStripMenuItem.Click += new System.EventHandler(this.sprachpluginsHinzufügenToolStripMenuItem_Click);
            // 
            // lbl_path
            // 
            this.lbl_path.AutoSize = true;
            this.lbl_path.Location = new System.Drawing.Point(13, 44);
            this.lbl_path.Name = "lbl_path";
            this.lbl_path.Size = new System.Drawing.Size(83, 13);
            this.lbl_path.TabIndex = 1;
            this.lbl_path.Text = "Pfad auswählen";
            // 
            // lbl_language
            // 
            this.lbl_language.AutoSize = true;
            this.lbl_language.Location = new System.Drawing.Point(339, 45);
            this.lbl_language.Name = "lbl_language";
            this.lbl_language.Size = new System.Drawing.Size(101, 13);
            this.lbl_language.TabIndex = 2;
            this.lbl_language.Text = "Sprache auswählen";
            // 
            // lbl_sources
            // 
            this.lbl_sources.AutoSize = true;
            this.lbl_sources.Location = new System.Drawing.Point(13, 92);
            this.lbl_sources.Name = "lbl_sources";
            this.lbl_sources.Size = new System.Drawing.Size(76, 13);
            this.lbl_sources.TabIndex = 3;
            this.lbl_sources.Text = "Sourcedateien";
            // 
            // lbl_preview
            // 
            this.lbl_preview.AutoSize = true;
            this.lbl_preview.Location = new System.Drawing.Point(339, 92);
            this.lbl_preview.Name = "lbl_preview";
            this.lbl_preview.Size = new System.Drawing.Size(52, 13);
            this.lbl_preview.TabIndex = 4;
            this.lbl_preview.Text = "Vorschau";
            // 
            // tb_path
            // 
            this.tb_path.Location = new System.Drawing.Point(16, 60);
            this.tb_path.Name = "tb_path";
            this.tb_path.ReadOnly = true;
            this.tb_path.Size = new System.Drawing.Size(261, 20);
            this.tb_path.TabIndex = 5;
            // 
            // btn_path
            // 
            this.btn_path.Location = new System.Drawing.Point(283, 58);
            this.btn_path.Name = "btn_path";
            this.btn_path.Size = new System.Drawing.Size(20, 23);
            this.btn_path.TabIndex = 6;
            this.btn_path.Text = "button1";
            this.btn_path.UseVisualStyleBackColor = true;
            this.btn_path.Click += new System.EventHandler(this.btn_path_Click);
            // 
            // cb_language
            // 
            this.cb_language.FormattingEnabled = true;
            this.cb_language.Location = new System.Drawing.Point(342, 59);
            this.cb_language.Name = "cb_language";
            this.cb_language.Size = new System.Drawing.Size(235, 21);
            this.cb_language.TabIndex = 7;
            // 
            // lb_sourcefiles
            // 
            this.lb_sourcefiles.FormattingEnabled = true;
            this.lb_sourcefiles.Location = new System.Drawing.Point(16, 109);
            this.lb_sourcefiles.Name = "lb_sourcefiles";
            this.lb_sourcefiles.Size = new System.Drawing.Size(287, 238);
            this.lb_sourcefiles.TabIndex = 8;
            // 
            // tv_preview
            // 
            this.tv_preview.Location = new System.Drawing.Point(342, 109);
            this.tv_preview.Name = "tv_preview";
            this.tv_preview.Size = new System.Drawing.Size(235, 238);
            this.tv_preview.TabIndex = 9;
            // 
            // btn_startAnalyze
            // 
            this.btn_startAnalyze.Location = new System.Drawing.Point(460, 354);
            this.btn_startAnalyze.Name = "btn_startAnalyze";
            this.btn_startAnalyze.Size = new System.Drawing.Size(116, 23);
            this.btn_startAnalyze.TabIndex = 10;
            this.btn_startAnalyze.Text = "Analyse starten";
            this.btn_startAnalyze.UseVisualStyleBackColor = true;
            this.btn_startAnalyze.Click += new System.EventHandler(this.btn_startAnalyze_Click);
            // 
            // CodeAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 389);
            this.Controls.Add(this.btn_startAnalyze);
            this.Controls.Add(this.tv_preview);
            this.Controls.Add(this.lb_sourcefiles);
            this.Controls.Add(this.cb_language);
            this.Controls.Add(this.btn_path);
            this.Controls.Add(this.tb_path);
            this.Controls.Add(this.lbl_preview);
            this.Controls.Add(this.lbl_sources);
            this.Controls.Add(this.lbl_language);
            this.Controls.Add(this.lbl_path);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CodeAnalyzer";
            this.Text = "CodeAnalyzer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speichernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem einstellungenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sprachpluginsHinzufügenToolStripMenuItem;
        private System.Windows.Forms.Label lbl_path;
        private System.Windows.Forms.Label lbl_language;
        private System.Windows.Forms.Label lbl_sources;
        private System.Windows.Forms.Label lbl_preview;
        private System.Windows.Forms.TextBox tb_path;
        private System.Windows.Forms.Button btn_path;
        private System.Windows.Forms.ComboBox cb_language;
        private System.Windows.Forms.ListBox lb_sourcefiles;
        private System.Windows.Forms.TreeView tv_preview;
        private System.Windows.Forms.Button btn_startAnalyze;
    }
}