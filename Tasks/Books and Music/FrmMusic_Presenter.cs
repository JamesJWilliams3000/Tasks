using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    class FrmMusic_Presenter
    {
        string basefold = @"C:\Users\howdy\Documents\Songs\000 Later\";

        public FrmMusic_Presenter()
        {
            DeleteMissingAlbums();
            AddNewAlbums();
        }

        void AddNewAlbums()
        {
            string[] folders = GetFolders();
            string[] fullfolders = GetFullFolders();            

            for (int i = 0; i < folders.Length; i++)
            {
                if (GetFileCount(fullfolders[i]) > 0)
                {
                    if (!MusicRepository.AlbumExists(folders[i]))
                    {
                        MusicRepository.Add(folders[i]);
                    }
                }
            }
        }

        void DeleteMissingAlbums()
        {            
            var albums = MusicRepository.GetAlbums();

            if (albums.Length > 0)
            {
                string[] folders = GetFolders();

                foreach (var album in albums)
                {
                    if (!folders.Contains(album.Path.Trim()))
                    {
                        MusicRepository.Delete(album);
                    }
                }
            }
        }

        public List<MusicRow> GetMusicRows()
        {
            string[] folders = GetFolders();
            string[] fullfolders = GetFullFolders();
            List<MusicRow> rows = new List<MusicRow>();

            for (int i = 0; i < folders.Length; i++)
            {
                int count = GetFileCount(fullfolders[i]);
                if (count > 0)
                {
                    Music music = MusicRepository.GetAlbum(folders[i]);
                    if (music != null)
                    {
                        rows.Add(new MusicRow(music, count));
                    }
                }
            }

            return rows;
        }

        string[] GetFolders()
        {
            string[] folders = Directory.GetDirectories(basefold, "*", SearchOption.AllDirectories);

            for (int i = 0; i < folders.Length; i++)
            {
                folders[i] = folders[i].Remove(0, basefold.Length);
            }
            return folders;
        }

        string[] GetFullFolders()
        {
            return Directory.GetDirectories(basefold, "*", SearchOption.AllDirectories);
        }

        int GetFileCount(string folder)
        {
            return Directory.GetFiles(folder, "*.mp3").Concat(Directory.GetFiles(folder, "*.m4a")).Count();
        }

        public List<TotalsRow> GetTotalRows(BindingList<MusicRow> albums)
        {
            List<TotalsRow> rows = new List<TotalsRow>();
            int total = albums.Sum(x => x.Count);
            var groups = albums.GroupBy(x => x.Type);

            rows.Add(new TotalsRow("Total", total, total));

            foreach (var group in groups)
            {
                rows.Add(new TotalsRow(group.Key, group.ToList().Sum(x => x.Count), total));
            }

            return rows;
        }

        public static List<NeedRow> GetNeedRows(BindingList<MusicRow> albums, int count)
        {
            List<NeedRow> rows = new List<NeedRow>();
            int total = albums.Sum(x => x.Count);
            var groups = albums.GroupBy(x => x.Type);

            rows.Add(new NeedRow("Need:", count));

            foreach (var group in groups)
            {
                int sum = group.ToList().Sum(x => x.Count);

                double Percent = (double)sum / total;

                rows.Add(new NeedRow(group.Key, Percent * count));
            }

            return rows;
        }
    }
}
