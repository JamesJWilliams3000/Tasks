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
    public partial class frmBooks : Form
    {
        int startPage = 0;
        int endPage = 0;

        public frmBooks()
        {
            InitializeComponent();
        }

        private void btnCatalogue_Click(object sender, EventArgs e)
        {
            var frm = new frmNewBooks();
            frm.Show();
        }

        private void frmBooks_Resize(object sender, EventArgs e)
        {
            flpPage.Width = Width - 100;
            flpPage.Height = Height - 100;

            flpPage.Location = new Point(this.Width / 2 - flpPage.Width / 2, this.Height / 2 - flpPage.Height / 2);
        }

        private void nudDays_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            int days = (int)nudDays.Value;

            dtpDeadline.ValueChanged -= dtpDeadline_ValueChanged;
            dtpDeadline.Value = now.AddDays(days);
            dtpDeadline.ValueChanged += dtpDeadline_ValueChanged;

            if ((int)nudEndPage.Value != 0)
            {
                CountValueChanged();
            }
        }

        private void dtpDeadline_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            int days = (int)(dtpDeadline.Value.Date - now.Date).TotalDays;

            nudDays.ValueChanged -= nudDays_ValueChanged;
            if (days > 0) nudDays.Value = days;
            nudDays.ValueChanged += nudDays_ValueChanged;

            if ((int)nudEndPage.Value != 0)
            {
                CountValueChanged();
            }
        }

        private void nudEndPages_ValueChanged(object sender, EventArgs e)
        {
            if (nudEndPage.Value > nudStartPage.Value)
            {
                CountValueChanged();

                lblCount.Text = string.Format("Count: {0:0.##}", nudEndPage.Value - nudStartPage.Value);
                endPage = (int)nudEndPage.Value;
            }
            else
            {
                nudEndPage.Value = endPage;
            }
        }

        private void nudStartPages_ValueChanged(object sender, EventArgs e)
        {
            if (nudStartPage.Value < nudEndPage.Value)
            {
                CountValueChanged();

                lblCount.Text = string.Format("Count: {0:0.##}", nudEndPage.Value - nudStartPage.Value);
                startPage = (int)nudStartPage.Value;
            }
            else
            {
                nudStartPage.Value = startPage;
            }

        }

        void CountValueChanged()
        {
            int count = (int)nudEndPage.Value - (int)nudStartPage.Value;
            SetPerDay(count);
        }

        void SetPerDay(int count)
        {
            int days = (int)nudDays.Value;
            double perDay = (double)count / days;
            nudPerDay.Value = (decimal)perDay;
        }

        int prevPerDay = 0;
        private void nudPerDay_ValueChanged(object sender, EventArgs e)
        {
            int count = (int)nudEndPage.Value - (int)nudStartPage.Value;

            if ((int)nudPerDay.Value * (int)nudDays.Value > 1000 || nudPerDay.Value > count)
            {
                nudPerDay.Value = prevPerDay;
            }

            prevPerDay = (int)nudPerDay.Value;
            
            if (nudPerDay.Value < count && (double)nudPerDay.Value > 0)
            {
                double days = count / (double)nudPerDay.Value;
                nudDays.Value = (int)days;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            JobRepository.AddBook(txtName.Text.Trim(), dtpDeadline.Value, (int)nudMinutes.Value, (double)nudPerDay.Value, (int)nudStartPage.Value, (int)nudEndPage.Value);
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                txtName.Focus();
                errorProvider1.SetError(txtName, "Enter name.");
            }
            else
            {
                errorProvider1.SetError(txtName, null);
            }
        }
    }
}
