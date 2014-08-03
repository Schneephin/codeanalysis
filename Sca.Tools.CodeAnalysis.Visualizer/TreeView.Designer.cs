namespace Sca.Tools.CodeAnalysis.Visualizer
{
    partial class TreeView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tvPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // tvPanel
            // 
            this.tvPanel.AutoScroll = true;
            this.tvPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvPanel.Location = new System.Drawing.Point(0, 0);
            this.tvPanel.Name = "tvPanel";
            this.tvPanel.Size = new System.Drawing.Size(701, 562);
            this.tvPanel.TabIndex = 0;
            // 
            // TreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvPanel);
            this.Name = "TreeView";
            this.Size = new System.Drawing.Size(701, 562);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tvPanel;
    }
}
