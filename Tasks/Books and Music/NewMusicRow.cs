using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tasks
{
    public partial class NewMusicRow : UserControl
    {
        public string Album { get; set; }

        public int Music_ID { get; set; }

        private int Count { get; set; }

        public string Type { get; set; }

        public NewMusicRow()
        {
            InitializeComponent();
        }

        public NewMusicRow(int music_ID, MusicRow row, int count)
        {
            InitializeComponent();

            Album = row.Album;
            Music_ID = music_ID;
            Count = count;
            Type = row.Type.Trim();

            lblAlbum.Text = Album;
            nudCount.Value = Count;
            nudCount.Maximum = row.Count;

            Margin = new Padding(0);
            nudCount.ValueChanged+= new EventHandler(nudCount_ValueChanged);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }

        public int GetCount()
        {
            return (int)nudCount.Value;
        }

        public delegate void ValueChangedHandler(object sender, EventArgs e);
        public event ValueChangedHandler ValueChanged;

        private void nudCount_ValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(sender, e);
        }
    }
}
