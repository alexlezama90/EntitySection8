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

            #region Lazy Loading
            //var course = context.Courses.Single(c => c.Id == 2);
            //        //Singleton methods like Single, SingleOrDefault, First, FirstOrDefault, 
            //        //Count, Max, Average, etc., cause an immediate query execution

            //foreach(var tag in course.Tags) //Thanks to the "virtual" keyword in the navigation property >Tags< of our Course Entity the Tags are not loaded immediatly
            //    Console.WriteLine(tag.Name); //The tags are queried when we iterate the Tags collection of the Course

            //
            // Lazy loading can be usefull in situation when loading a bunch of related objects can be costly.
            // Lazy loading Loads the Main objects and then loads the related objects on demand.
            // *AVOID LAZY LOADING IN WEB APPLICATIONS*

            //We can disable the Lazy loading in the entire context through adding a configuration in the Context class consntructor:
            //             this.Configuration.LazyLoadingEnabled = false;


            //Using Lazy loading innappropiatly can lead to  the N + 1 problem.
            //"In order to get N entities and their related entities, we'll end up with N + 1 queries" 
            #endregion

            #region N +1 Problem
            //var courses = context.Courses.ToList(); // Query No.1 (1)

            //foreach (var course in courses)
            //    Console.WriteLine("{0} by {1}", course.Name, course.Author.Name); //Entity Framework will execute a query per each course to get its author (N)

            //// N + 1 (Check SQL Profiler to check all the queries...
            #endregion

            #region Eager Loading
            //Eager loading is the opposite of Lazy loading 
            //Eager Loading loads everything up front to prevent additional queries to the database
            //var courses = context.Courses.ToList();
            //var courses = context.Courses.Include("Author").ToList(); //Magic strings
            //var courses = context.Courses.Include(c => c.Author).ToList();  //remember to import "using System.Data.Entity"

            //foreach(var course in courses)
            //    Console.WriteLine("{0} by {1}", course.Name, course.Author.Name); //Entity Framework will execute a query per each course to get its author (N)


            //For single properties***
            //context.Courses.Include(c => c.Author.Address);

            //For collection properties****
            //context.Courses.Include(a => a.Tags).Select(t => t.Cover);

            //Uses JOINs and will only do one roundtrip (consultará la bd una vez [hará de golpe todas las búsquedas])
            #endregion

            #region Explicit Loading
            //Will do separete queries and will do multiple round-trips

            #region Eager Loading Version
            //var author = context.Authors.Include(a => a.Courses).Single(a => a.Id == 1);

            //foreach(var course in author.Courses)
            //    Console.WriteLine("{0}", course.Name);
            #endregion

            //Explicit version
            //var author = context.Authors.Single(a => a.Id == 1);
            //////MSDN way //Just works for single entries (1 author)
            ////context.Entry(author).Collection(a => a.Courses).Load();
            ////context.Entry(author).Collection(a => a.Courses).Query().Where(c => c.FullPrice == 0).Load();

            ////Mosh Way
            ////context.Courses.Where(c => c.AuthorId == author.Id).Load();
            //context.Courses.Where(c => c.AuthorId == author.Id && c.FullPrice == 0).Load();

            //foreach (var course in author.Courses)
            //    Console.WriteLine("{0}", course.Name);

            var authors = context.Authors.ToList();
            var authorIds = authors.Select(c => c.Id);

            context.Courses.Where(c => authorIds.Contains(c.AuthorId) && c.FullPrice == 0).Load();

            #endregion

        }
    }
}
