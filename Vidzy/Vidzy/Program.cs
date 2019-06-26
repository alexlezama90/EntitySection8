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

            AddTags("classics", "drama");

            AddTagsToVideo(1, "classics", "drama", "comedy");

            RemoveTagsFromVideo(1, "comedy");

            RemoveVideo(1);

            RemoveGenre(1, true);

        }



        public static void AddVideo(Video video)
        {
            using (var context = new VidzyContext())
            {
                context.Videos.Add(video);
                context.SaveChanges();
            }
        }


        public static void AddTags(params string[] tagNames)
        {
            using (var context = new VidzyContext())
            {
                var tags = context.Tags.Where(t => tagNames.Contains(t.Name)).ToList();

                foreach (var name in tagNames)
                {
                    if (!tags.Any(t => t.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
                        context.Tags.Add(new Tag { Name = name });
                }

                context.SaveChanges();
            }
        }

        public static void AddTagsToVideo(int videoId, params string[] tagNames)
        {
            using (var context = new VidzyContext())
            {
                var tags = context.Tags.Where(t => tagNames.Contains(t.Name)).ToList();

                foreach(var tagName in tagNames)
                {
                    if (!tags.Any(t => t.Name.Equals(tagName, StringComparison.CurrentCultureIgnoreCase)))
                        tags.Add(new Tag { Name = tagName });
                }

                var video = context.Videos.Single(v => v.Id == videoId);

                tags.ForEach(t => video.AddTag(t));

                context.SaveChanges();
            }
        }

        public static void RemoveTagsFromVideo(int videoId, params string[] tagNames)
        {
            using (var context = new VidzyContext())
            {
                context.Tags.Where(t => tagNames.Contains(t.Name)).Load();

                var video = context.Videos.Single(v => v.Id == videoId);

                foreach(var tagName in tagNames)
                {
                    video.RemoveTag(tagName);
                }

                context.SaveChanges();
            }
        }

        public static void RemoveVideo(int videoId)
        {
            using (var context = new VidzyContext())
            {
                var video = context.Videos.SingleOrDefault(v => v.Id == videoId);
                if (video == null)
                    return;

                context.Videos.Remove(video);
                context.SaveChanges();
            }
        }

        public static void RemoveGenre(int genreId, bool enforceDeletingVideos)
        {
            using (var context = new VidzyContext())
            {
                var genre = context.Genres.Include(g => g.Videos).SingleOrDefault(g => g.Id == genreId);
                if (genre == null)
                    return;

                if (enforceDeletingVideos)
                    context.Videos.RemoveRange(genre.Videos);

                context.Genres.Remove(genre);
                context.SaveChanges();
            }
        }

    }
}
