using System;
using System.Linq;
using System.Data.Entity;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();

            //var authors = context.Authors.ToList();       To mucho code => var author = context.Authors.Single(a => a.Id == 1);
            //var author = authors.Single(a => a.Id == 1);

            var course = new Course
            {
                Name = "New Course",
                Description = "New Description",
                FullPrice = 19.95f,
                Level = 1,
                //Author = new Author { Id = 1, Name = "Mosh Hamedani" }
                //Author = author
                AuthorId = 1

            };

            context.Courses.Add(course);

            context.SaveChanges();

        }
    }
}
