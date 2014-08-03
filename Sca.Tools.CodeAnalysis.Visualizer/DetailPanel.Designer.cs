namespace Sca.Tools.CodeAnalysis.Visualizer
{
    partial class DetailPanel
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
            this.label1 = new System.Windows.Forms.Label();
            this.cb_selectNode = new System.Windows.Forms.ComboBox();
            this.lbl_selectNode = new System.Windows.Forms.Label();
            this.lv_results = new System.Windows.Forms.ListView();
            this.Detail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Details";
            // 
            // cb_selectNode
            // 
            this.cb_selectNode.FormattingEnabled = true;
            this.cb_selectNode.Location = new System.Drawing.Point(7, 17);
            this.cb_selectNode.Name = "cb_selectNode";
            this.cb_selectNode.Size = new System.Drawing.Size(390, 21);
            this.cb_selectNode.TabIndex = 3;
            this.cb_selectNode.SelectedIndexChanged += new System.EventHandler(this.cb_selectNode_SelectedIndexChanged);
            // 
            // lbl_selectNode
            // 
            this.lbl_selectNode.AutoSize = true;
            this.lbl_selectNode.Location = new System.Drawing.Point(4, 0);
            this.lbl_selectNode.Name = "lbl_selectNode";
            this.lbl_selectNode.Size = new System.Drawing.Size(78, 13);
            this.lbl_selectNode.TabIndex = 2;
            this.lbl_selectNode.Text = "Select Element";
            // 
            // lv_results
            // 
            this.lv_results.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Detail,
            this.Value});
            this.lv_results.Location = new System.Drawing.Point(7, 68);
            this.lv_results.Name = "lv_results";
            this.lv_results.Size = new System.Drawing.Size(387, 511);
            this.lv_results.TabIndex = 4;
            this.lv_results.UseCompatibleStateImageBehavior = false;
            this.lv_results.View = System.Windows.Forms.View.Details;
            // 
            // Detail
            // 
            this.Detail.Text = "Detail";
            this.Detail.Width = 340;
            // 
            // Value
            // 
            this.Value.Text = "Value";
            this.Value.Width = 40;
            // 
            // DetailPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lv_results);
            this.Controls.Add(this.cb_selectNode);
            this.Controls.Add(this.lbl_selectNode);
            this.Controls.Add(this.label1);
            this.Name = "DetailPanel";
            this.Size = new System.Drawing.Size(400, 600);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_selectNode;
        private System.Windows.Forms.Label lbl_selectNode;
        private System.Windows.Forms.ListView lv_results;
        private System.Windows.Forms.ColumnHeader Detail;
        private System.Windows.Forms.ColumnHeader Value;
    }
}
