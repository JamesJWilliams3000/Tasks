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
    public partial class frmMusic : Form
    {
        public BindingList<MusicRow> Rows { get; set; }
        public BindingList<TotalsRow> TotalRows { get; set; }
        public BindingList<NeedRow> NeedRows { get; set; }

        int prevDays = 0;
        int prevDeadline = 1;

        public frmMusic()
        {
            InitializeComponent();            
        }

        private void frmMusic_Load(object sender, EventArgs e)
        {
            DisplayCurrentMusic();
        }

        void DisplayCurrentMusic()
        {
            SuspendLayout();
            using (var context = new JOBSEntities())
            {
                if (context.Current_Music.Count() > 0)
                {
                    var job = context.Jobs.FirstOrDefault(x => x.Job_ID == context.Current_Music.FirstOrDefault().Job_ID);
                    var sub = context.SubJobs.FirstOrDefault(x => x.Job_ID == job.Job_ID);
                    lblEndDate.Text += job.Deadline.Value.ToShortDateString();
                    lblCount.Text += sub.Count;
                    lblPerDay.Text += sub.Per_Day.ToString("0.#");

                    var albums = context.Current_Music.Where(x => x.Job_ID == job.Job_ID).ToList();
                    foreach (var album in albums)
                    {
                        flpCurrentAlbums.Controls.Add(new Label() { Text = album.Count + " - " + album.Album.Trim() });
                    }

                    pnlCurrentMusic.Visible = true;
                    pnlNew.Visible = false;
                }
            }
            ResumeLayout();
        }

        public void SetBinding()
        {
            Rows.AllowEdit = true;
            dgv.AutoGenerateColumns = true;

            dgv.DataSource = Rows;
            dgv.AutoResizeColumns();
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns[0].Width = 200;
            dgv.ColumnHeaderMouseClick += Dgv_ColumnHeaderMouseClick;


            TotalRows.AllowEdit = true;
            dgvTotals.AutoGenerateColumns = true;

            dgvTotals.DataSource = TotalRows;
            dgvTotals.AutoResizeColumns();
            dgvTotals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvTotals.Columns[2].DefaultCellStyle.Format = "P2";

            dgvTotals.Height = dgvTotals.Rows.GetRowsHeight(DataGridViewElementStates.None) + dgvTotals.ColumnHeadersHeight;
        }

        private void Dgv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    dgv.DataSource = new BindingList<MusicRow>(Rows.OrderBy(x => x.Folder).ToList());
                    break;
                case 1:
                    dgv.DataSource = new BindingList<MusicRow>(Rows.OrderBy(x => x.Album).ToList());
                    break;
                case 2:
                    dgv.DataSource = new BindingList<MusicRow>(Rows.OrderBy(x => x.Type).ToList());
                    break;
                case 3:
                    dgv.DataSource = new BindingList<MusicRow>(Rows.OrderBy(x => x.Count).ToList());
                    break;
                default:
                    break;
            }
        }

        public void SetNeedBinding()
        {
            NeedRows.AllowEdit = true;
            dgvNeed.AutoGenerateColumns = true;

            dgvNeed.DataSource = NeedRows;
            dgvNeed.AutoResizeColumns();
            dgvNeed.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgvNeed.Columns[1].DefaultCellStyle.Format = "0.00";
            dgvNeed.Height = dgvNeed.Rows.GetRowsHeight(DataGridViewElementStates.None) + dgvNeed.ColumnHeadersHeight + 50;
        }

        private void frmMusic_Shown(object sender, EventArgs e)
        {
            SuspendLayout();
            var pres = new FrmMusic_Presenter();
            Rows = new BindingList<MusicRow>(pres.GetMusicRows());
            TotalRows = new BindingList<TotalsRow>(pres.GetTotalRows(Rows));
            NeedRows = new BindingList<NeedRow>(FrmMusic_Presenter.GetNeedRows(Rows, (int)nudCount.Value));

            SetNeedBinding();
            SetBinding();

            ResumeLayout();
            flpPage.Visible = true;
        }

        private void frmMusic_Resize(object sender, EventArgs e)
        {
            flpPage.Width = Width - 100;
            flpPage.Height = Height - 100;
            dgv.Height = flpPage.Height - 50;
            flpMusic.Height = flpPage.Height;

            flpPage.Location = new Point(this.Width / 2 - flpPage.Width / 2, this.Height / 2 - flpPage.Height / 2);
            
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var row = (MusicRow)dgv.Rows[e.RowIndex].DataBoundItem;

            MusicRepository.Update(row.GetMusic(), row.Type);
            //TODO update totals
            dgv.Refresh();
        }

        private void nudDays_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            int days = (int)nudDays.Value;

            dtpDeadline.ValueChanged -= dtpDeadline_ValueChanged;
            dtpDeadline.Value = now.AddDays(days);
            dtpDeadline.ValueChanged += dtpDeadline_ValueChanged;

            if ((int)nudPerDay.Value * (int)nudDays.Value > 1000)
            {
                nudDays.Value = prevDeadline;
            }
            else
            {
                SetNewCount();
            }

            prevDeadline = (int)nudDays.Value;
        }

        private void dtpDeadline_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            int days = (int)(dtpDeadline.Value.Date - now.Date).TotalDays;

            nudDays.ValueChanged -= nudDays_ValueChanged;
            if (days > 0) nudDays.Value = days;
            nudDays.ValueChanged += nudDays_ValueChanged;

        }

        private void btnNewMusicToggle_Click(object sender, EventArgs e)
        {
            pnlNewMusic.Visible = true;
            btnAddAlbum.Enabled = true;
        }

        private void btnAddAlbum_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                var row = (MusicRow)dgv.SelectedRows[0].DataBoundItem;

                int count = 0;
                if (rbAll.Checked)
                {
                    count = row.Count;
                }
                else
                {
                    count = (int)nudNewCount.Value;
                }

                var control = new NewMusicRow(row.GetMusic().Music_ID, row, count);
                control.ValueChanged += NewMusicValueChanged;
                flpAlbums.Controls.Add(control);
                SetTotal();
                NeedAmountChange();
            }
        }

        private void NewMusicValueChanged(object sender, EventArgs e)
        {
            NeedAmountChange();
            SetTotal();
        }


        private void nudPerDay_ValueChanged(object sender, EventArgs e)
        {           
            
            if ((int)nudPerDay.Value * (int)nudDays.Value > 1000)
            {
                nudPerDay.Value = prevDays;
            }
            else
            {
                SetNewCount();
            }

            prevDays = (int)nudPerDay.Value;
        }

        void SetNewCount()
        {
            if ((int)nudPerDay.Value != 0)
            {
                nudCount.Value = (int)nudPerDay.Value * (int)nudDays.Value;
            }
        }

        private void frmMusic_ControlRemoved(object sender, ControlEventArgs e)
        {
        }

        void SetTotal()
        {
            nudTotal.Value = flpAlbums.Controls.OfType<NewMusicRow>().ToList().Sum(x => x.GetCount());
        }

        private void flpAlbums_ControlRemoved(object sender, ControlEventArgs e)
        {
            SetTotal();
            NeedAmountChange();
        }

        private void nudCount_ValueChanged(object sender, EventArgs e)
        {
            NeedRows.Clear();
            NeedRows = new BindingList<NeedRow>(FrmMusic_Presenter.GetNeedRows(Rows, (int)nudCount.Value));
            SetNeedBinding();
        }

        void NeedAmountChange()
        {
            NeedRows.Clear();
            NeedRows = new BindingList<NeedRow>(FrmMusic_Presenter.GetNeedRows(Rows, (int)nudCount.Value));
            SetNeedBinding();

            var groups = flpAlbums.Controls.OfType<NewMusicRow>().ToList().GroupBy(x => x.Type);

            foreach (var group in groups)
            {
                var sum = group.ToList().Sum(x => x.GetCount());
                var needRow = NeedRows.First(x => x.Type.Trim() == group.Key.Trim());
                needRow.Amount -= sum;
            }

            dgvNeed.Refresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Job job = JobRepository.AddMusicJob("Music", dtpDeadline.Value);
            SubJob sub = JobRepository.AddMusicSubJob(job.Job_ID, (int)nudTotal.Value, (double)nudPerDay.Value);

            var albums = flpAlbums.Controls.OfType<NewMusicRow>().ToList();
            var music = new List<Current_Music>();
            int i = JobRepository.GetLastCurrentMusicID() + 1;
            foreach (var album in albums)
            {
                music.Add(new Current_Music() { ID = i, Job_ID = job.Job_ID ,Count = album.GetCount(), Album = album.Album.Trim(), Path = JobRepository.GetPath(album.Music_ID) });
                i++;
            }
            JobRepository.AddCurrentMusic(music);   
            
            if (DateRepository.GetLastDate().Date1.Date == DateTime.Now.Date)
            {
                JobRepository.AddToDate(DateRepository.GetLastDate().Date_ID, job, sub);
            }

            MoveMusic(music);
            DisplayCurrentMusic();
        }

        void MoveMusic(List<Current_Music> albums)
        {
            string songs = @"C:\Users\howdy\Documents\Songs\000 Later\";
            //to
            string music = @"C:\Users\howdy\Documents\Music\";

            foreach (var album in albums)
            {                
                if (album.Path.Split(new[] { '\\' }).Length > 1)
                {
                    if (!Directory.Exists(music + album.Path))
                    {
                        Directory.CreateDirectory(music + album.Path);
                        Directory.Delete(music + album.Path); //deletes the subfolder so dir can be moved without ex
                    }
                }

                Directory.Move(songs + album.Path, music + album.Path);
            }
        }

    }
}
