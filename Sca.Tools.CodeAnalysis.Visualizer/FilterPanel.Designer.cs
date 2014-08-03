namespace Sca.Tools.CodeAnalysis.Visualizer
{
    partial class FilterPanel
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
            this.lbl_selectNode = new System.Windows.Forms.Label();
            this.cb_selectNode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_selectNode
            // 
            this.lbl_selectNode.AutoSize = true;
            this.lbl_selectNode.Location = new System.Drawing.Point(4, 4);
            this.lbl_selectNode.Name = "lbl_selectNode";
            this.lbl_selectNode.Size = new System.Drawing.Size(78, 13);
            this.lbl_selectNode.TabIndex = 0;
            this.lbl_selectNode.Text = "Select Element";
            // 
            // cb_selectNode
            // 
            this.cb_selectNode.FormattingEnabled = true;
            this.cb_selectNode.Location = new System.Drawing.Point(7, 21);
            this.cb_selectNode.Name = "cb_selectNode";
            this.cb_selectNode.Size = new System.Drawing.Size(390, 21);
            this.cb_selectNode.TabIndex = 1;
            this.cb_selectNode.SelectedIndexChanged += new System.EventHandler(this.cb_selectNode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filters";
            // 
            // FilterPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_selectNode);
            this.Controls.Add(this.lbl_selectNode);
            this.Name = "FilterPanel";
            this.Size = new System.Drawing.Size(400, 600);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_selectNode;
        private System.Windows.Forms.ComboBox cb_selectNode;
        private System.Windows.Forms.Label label1;
    }
}
