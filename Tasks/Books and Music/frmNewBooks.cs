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
    public partial class frmNewBooks : Form
    {
        public frmNewBooks()
        {
            InitializeComponent();

            openFileDialog1.InitialDirectory = @"C:\\";
            openFileDialog1.Filter = "Txt Files (*.txt)|*.txt";
            openFileDialog1.Multiselect = false;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] fileContent = File.ReadAllLines(openFileDialog1.FileName);
                    lblFilePath.Text = "Adding books...";
                    AddBooks(fileContent);

                    string message = string.Format("Books Added. Delete {0}?", openFileDialog1.FileName);
                    if (MessageBox.Show(message, "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        File.Delete(openFileDialog1.FileName);
                        message = "File Deleted. ";
                    }
                    else
                    {
                        message = "";
                    }

                    message += "Closing window.";
                    MessageBox.Show(message);
                    Close();

                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }

                openFileDialog1.Dispose();
            }
        }

        void AddBooks(string[] fileContent)
        {
            foreach (var line in fileContent)
            {
                string[] contents = line.Split('|');
                string title = contents[0].Length > 300 ? contents[0].Substring(0, 300) : contents[0].Trim();
                int pages;
                if (!int.TryParse(contents[1], out pages))
                {
                    pages = 0;
                }

                DateTime bought;

                if (!DateTime.TryParse(contents[2], out bought))
                {
                    bought = DateTime.Now;
                }

                BookRepository.Add(title, pages, bought);                
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();

            int pages = (int)nudPages.Value;

            DateTime bought = dtpBought.Value;

            BookRepository.Add(title, pages, bought);

            MessageBox.Show("Book added. Closing window.");
            Close();
        }
    }
}
