using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tasks
{
    public partial class frmAddFolder : Form
    {
        public frmAddFolder()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (Directory.Exists(txtPath.Text.Trim()))
            {
                using (var context = new JOBSEntities())
                {
                    int id = context.Folders.Count() > 0 ? context.Folders.Last().ID + 1 : 0;
                    var folder = new Folder()
                    {
                        ID = id,
                        Name = txtName.Text.Trim(),
                        Path = txtPath.Text.Trim()
                    };

                    context.Folders.Add(folder);
                    context.SaveChanges();
                }

                Close();
            }
            else
            {
                MessageBox.Show("Directory does not exist.");
            }
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextbox(txtName, "Name");
        }

        private void txtPath_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextbox(txtPath, "Path");
        }

        private void ValidateTextbox(TextBox txt, string value)
        {
            if (string.IsNullOrEmpty(txt.Text))
            {
                txt.Focus();
                errorProvider1.SetError(txt, string.Format("Enter {0}.", value));
            }
            else
            {
                errorProvider1.SetError(txt, null);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
