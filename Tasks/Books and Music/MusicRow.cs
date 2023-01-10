using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    public class MusicRow
    {
        private Music Music { get; set; }
      
        public string Folder { get; set; }

        public string Album { get; set; }

        public string Type { get; set; }

        public int Count { get; set; }

        public MusicRow(Music music, int count)
        {
            music.Path = music.Path.Trim();
            music.Type = music.Type.Trim();

            Music = music;
            Count = count;

            Album = music.Path.Contains(@"\") ? music.Path.Substring(music.Path.LastIndexOf(@"\") + 1) : music.Path;
            Folder = music.Path.Substring(0, music.Path.Length - Album.Length);
            Type = music.Type.Trim();
        }

        public Music GetMusic()
        {
            return Music;
        }
       
        //Console.WriteLine("{0:D2} - [{1}] {2}", fileCount, folderpath, name);

    }
}
