using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tasks
{
    public partial class frmContainer : Form
    {
        public frmContainer()
        {
            InitializeComponent();
        }

        void OpenForm(Form frm)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }

            frm.MdiParent = this;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void frmContainer_Load(object sender, EventArgs e)
        {
            
        }

        private void addTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmAddEditJob());
        }
    }

}
