using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sca.Tools.CodeAnalysis.Visualizer
{
    internal partial class ButtonPanel : UserControl
    {
        private Control ctrl;

        internal ButtonPanel(Control ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;
        }

        private void btn_classView_Click(object sender, EventArgs e)
        {
            this.btn_classView.Enabled = false;
            this.btn_functionView.Enabled = true;
            this.ctrl.doAction(Actions.SwitchToClassMode);
        }

        private void btn_functionView_Click(object sender, EventArgs e)
        {
            this.btn_classView.Enabled = true;
            this.btn_functionView.Enabled = false;
            this.ctrl.doAction(Actions.SwitchToFunctionMode);
        }
    }
}
