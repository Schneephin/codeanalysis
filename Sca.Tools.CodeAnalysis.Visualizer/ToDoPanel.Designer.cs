namespace Sca.Tools.CodeAnalysis.Visualizer
{
    partial class ToDoPanel
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
            this.cb_type = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lv_results = new System.Windows.Forms.ListView();
            this.Function = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select type";
            // 
            // cb_type
            // 
            this.cb_type.FormattingEnabled = true;
            this.cb_type.Location = new System.Drawing.Point(7, 21);
            this.cb_type.Name = "cb_type";
            this.cb_type.Size = new System.Drawing.Size(390, 21);
            this.cb_type.TabIndex = 1;
            this.cb_type.SelectedIndexChanged += new System.EventHandler(this.cb_type_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Top Results";
            // 
            // lv_results
            // 
            this.lv_results.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Function,
            this.Value});
            this.lv_results.Location = new System.Drawing.Point(10, 86);
            this.lv_results.Name = "lv_results";
            this.lv_results.Size = new System.Drawing.Size(387, 511);
            this.lv_results.TabIndex = 3;
            this.lv_results.UseCompatibleStateImageBehavior = false;
            this.lv_results.View = System.Windows.Forms.View.Details;
            // 
            // Function
            // 
            this.Function.Text = "Function";
            this.Function.Width = 340;
            // 
            // Value
            // 
            this.Value.Text = "Value";
            this.Value.Width = 40;
            // 
            // ToDoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lv_results);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cb_type);
            this.Controls.Add(this.label1);
            this.Name = "ToDoPanel";
            this.Size = new System.Drawing.Size(400, 600);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_type;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lv_results;
        private System.Windows.Forms.ColumnHeader Function;
        private System.Windows.Forms.ColumnHeader Value;
    }
}
