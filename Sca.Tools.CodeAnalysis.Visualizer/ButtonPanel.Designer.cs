namespace Sca.Tools.CodeAnalysis.Visualizer
{
    partial class ButtonPanel
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
            this.btn_classView = new System.Windows.Forms.Button();
            this.btn_functionView = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_classView
            // 
            this.btn_classView.Enabled = false;
            this.btn_classView.Location = new System.Drawing.Point(538, 3);
            this.btn_classView.Name = "btn_classView";
            this.btn_classView.Size = new System.Drawing.Size(86, 23);
            this.btn_classView.TabIndex = 0;
            this.btn_classView.Text = "Class View";
            this.btn_classView.UseVisualStyleBackColor = true;
            this.btn_classView.Click += new System.EventHandler(this.btn_classView_Click);
            // 
            // btn_functionView
            // 
            this.btn_functionView.Location = new System.Drawing.Point(446, 3);
            this.btn_functionView.Name = "btn_functionView";
            this.btn_functionView.Size = new System.Drawing.Size(86, 23);
            this.btn_functionView.TabIndex = 1;
            this.btn_functionView.Text = "Function View";
            this.btn_functionView.UseVisualStyleBackColor = true;
            this.btn_functionView.Click += new System.EventHandler(this.btn_functionView_Click);
            // 
            // ButtonPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_functionView);
            this.Controls.Add(this.btn_classView);
            this.Name = "ButtonPanel";
            this.Size = new System.Drawing.Size(629, 43);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_classView;
        private System.Windows.Forms.Button btn_functionView;
    }
}
