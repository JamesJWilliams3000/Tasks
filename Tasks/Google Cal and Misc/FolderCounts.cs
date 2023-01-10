using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Tasks
{
    public partial class FolderCounts : UserControl
    {

        public List<Folder> Folders { get; set; }

        public FolderCounts()
        {
            InitializeComponent();
            Folders = GetFolders();
            FillList();
        }

        public FolderCounts(List<Folder> folders)
        {
            InitializeComponent();
            Folders = folders;
            FillList();
        }

        private List<Folder> GetFolders()
        {
            using (var context = new JOBSEntities())
            {
                if (context.Folders.Count() > 0)
                {
                    return context.Folders.ToList();
                }
                else
                {
                    return new List<Folder>();
                }
            }
        }

        private void FillList()
        {
            SuspendLayout();
            flpFolders.Controls.Clear();
            foreach (var folder in Folders)
            {               
                if (Directory.Exists(folder.Path))
                {
                    int count = Directory.GetFiles(folder.Path, "*", SearchOption.AllDirectories).Length;
                    var lbl = new Label() { Text = string.Format("{0}: {1}", folder.Name.Trim(), count), AutoSize = true };
                    flpFolders.Controls.Add(lbl);
                    lbl.Show();
                    //flpFolders.Controls.Add(new Label() { Text = string.Format("{0}: {1}", folder.Name.Trim(), count)} );
                }                    
            }

            ResumeLayout();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillList();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("chrome", "chrome://bookmarks/");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenForm(new frmAddFolder());
        }

        void OpenForm(Form frm)
        {
            frm.MdiParent = this.FindForm().MdiParent;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.BeginInvoke(new MethodInvoker(this.FindForm().Close));
        }

        private void FolderCounts_Load(object sender, EventArgs e)
        {
            flpFolders.MinimumSize = new Size(Width - 10, 0);
        }
    }
}
