using System;
using System.Linq;
using System.Data.Entity;

namespace Vidzy
{
    class Program
    {
        static void Main(string[] args)
        {
            AddVideo(new Video
            {
                Name = "Terminator 1",
                GenreId = 2,
                Classification = Classification.Silver,
                ReleaseDate = new DateTime(1984, 10, 26)
            });

        }

        public static void AddVideo(Video video)
        {
            using (var context = new VidzyContext())
            {
                context.Videos.Add(video);
                //context.SaveChanges();
            }
        }

        public static void AddTag(Tag tag)
        {
            using (var context = new VidzyContext())
            {
                context.Tags.Add(tag);
            }
        }

    }
}
