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

            #region Adding Objects
            //var authors = context.Authors.ToList();       To mucho code => var author = context.Authors.Single(a => a.Id == 1);
            //var author = authors.Single(a => a.Id == 1);

            //var course = new Course
            //{
            //    Name = "New Course",
            //    Description = "New Description",
            //    FullPrice = 19.95f,
            //    Level = 1,
            //    //Author = new Author { Id = 1, Name = "Mosh Hamedani" }
            //    //Author = author
            //    AuthorId = 1

            //};

            //context.Courses.Add(course);
            //context.SaveChanges();

            #endregion

            #region Updating Objects

            //var course = context.Courses.Find(4); //Single(c => c.Id == 4)

            //course.Name = "New Name";
            //course.AuthorId = 2;

            //context.SaveChanges();

            #endregion

            #region Removing Objects

            ////Cascade Example //This will delete all the Tags related to the course with Id = 6
            //var course = context.Courses.Find(6);
            //context.Courses.Remove(course);

            ////No Cascade Example
            //var author = context.Authors.Include(a => a.Courses).Single(a => a.Id == 2);
            //context.Courses.RemoveRange(author.Courses);
            //context.Authors.Remove(author);

            //context.SaveChanges();

            //Prefer LOGICAL deletes to PHYSICAL deletes. (use Flags)
            //author.IsDeleted = true;

            #endregion

            #region Working with Change Tracker
            ////Add an object
            //context.Authors.Add(new Author { Name = "New Author"});

            ////Update an object
            //var author = context.Authors.Find(3);
            //author.Name = "Updated";

            //Remove an object
            //var another = context.Authors.Find(4);
            //context.Authors.Remove(another);

            //var entries = context.ChangeTracker.Entries(); ////context.ChangeTracker.Entries<Author>(); All >Author< entries.


            //foreach(var entry in entries)       //entry.Reload();
            //    Console.WriteLine(entry.State);

            //context.SaveChanges();
            #endregion


        }
    }
}
