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
            var presenter = new ViewTasksPresenter(Get_ViewTasksView());
            presenter.Show();
        }

        private void addTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new AddEditJobView());
        }

        private void editTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmChooseJob(true));
        }

        private void deleteTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmChooseJob(false));
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //OpenForm(new frmViewJobs());
            var presenter = new ViewTasksPresenter(Get_ViewTasksView());
            presenter.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            OpenForm(new Testing());
        }

        private void musicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmMusic());
        }

        private void booksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmBooks());
        }

        public IViewTasksView Get_ViewTasksView()
        {
            return SetupForm(new ViewJobs()) as IViewTasksView;
        }

        Form SetupForm(Form frm)
        {
            frm.MdiParent = this;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            return frm;
        }
    }

}
