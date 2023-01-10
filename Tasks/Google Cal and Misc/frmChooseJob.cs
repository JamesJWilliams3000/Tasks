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
    public partial class frmChooseJob : Form
    {
        public frmChooseJob()
        {
            InitializeComponent();
        }

        public frmChooseJob(bool edit)
        {
            InitializeComponent();
            Edit = edit;

            using (var context = new JOBSEntities())
            {
                cboJob.BindingContext = new BindingContext();
                cboJob.ValueMember = "Job_ID";
                cboJob.DisplayMember = "Name";
                cboJob.DataSource = context.Jobs.ToList();
            }

            btnAdd.Text = Edit ? "Edit" : "Delete";
        }

        public bool Edit { get; set; }

        private void frmChooseJob_Resize(object sender, EventArgs e)
        {
            pnlPage.Location = new Point(this.Width / 2 - pnlPage.Width / 2, this.Height / 2 - pnlPage.Height / 2);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Job job = (Job)cboJob.SelectedItem;
            string message = string.Format("Are you sure you want to {0} {1}?", Edit ? "edit" : "delete" , job.Name.Trim());
            if (MessageBox.Show(message, "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (Edit)
                {
                    //open the edit form
                }
                else
                {
                    JobRepository.Delete(job);
                }

                Close();
            }
        }
    }
}
