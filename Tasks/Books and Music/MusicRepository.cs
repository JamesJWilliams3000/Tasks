using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    public class MusicRepository
    {
        public static bool AlbumExists(string path)
        {
            using (var context = new JOBSEntities())
            {
                var album = context.Musics.FirstOrDefault(x => x.Path.Trim() == path.Trim());

                if (album != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void Add(string path)
        {
            Music music = new Music()
            {
                Music_ID = GetNewID(),
                Path = path, 
                Type = "no type"
            };

            using (var context = new JOBSEntities())
            {
                context.Musics.Add(music);
                context.SaveChanges();
            }
        } 
        
        public static Music[] GetAlbums()
        {
            using (var context = new JOBSEntities())
            {
                return context.Musics.ToArray();
            }
        }

        public static Music GetAlbum(string path)
        {
            using (var context = new JOBSEntities())
            {
                return context.Musics.FirstOrDefault(x => x.Path == path);
            }
        }

        public static int GetLastID()
        {
            using (var context = new JOBSEntities())
            {
                if (context.Musics.Count() > 0)
                {
                    return context.Musics.OrderByDescending(x => x.Music_ID).FirstOrDefault().Music_ID;
                }
                else
                {
                    return -1;
                }
            }
        }

        public static int GetNewID()
        {
            return GetLastID() + 1;
        }

        public static void Delete(Music music)
        {
            using (var context = new JOBSEntities())
            {
                if (context.Entry(music).State == EntityState.Detached)
                {
                    context.Musics.Attach(music);
                }

                context.Musics.Remove(music);
                context.SaveChanges();
            }
        }

        public static void Delete(List<Music> music)
        {
            using (var context = new JOBSEntities())
            {
                music.ForEach(x =>
                {
                    if (context.Entry(x).State == EntityState.Detached)
                    {
                        context.Musics.Attach(x);
                    }
                });

                context.Musics.RemoveRange(music);
                context.SaveChanges();
            }
        }

        public static void Update(Music music, string type)
        {
            using (var context = new JOBSEntities())
            {
                if (context.Entry(music).State == EntityState.Detached)
                {
                    context.Musics.Attach(music);
                }

                music.Type = type;

                context.SaveChanges();
            }

        }
    }
}
