using System;
using System.Collections.Generic;
using System.Linq;

namespace funwithlinq
{
    class Program
    {
        static void Main()
        {
            // Specify the data source. 
            int[] scores = new int[] { 97, 92, 81, 60 };


            /*
             * Define the query expression. Lets just itterate over an array of
             * ints for this example.
             */

            IEnumerable<int> scoreQuery =
                from score in scores
                where score > 80
                select score;

            IEnumerable<int>scores2 =
                from score in scores
                where score > 80
                select score;

            // Execute the query.
            Console.WriteLine("Lets show scores above 80!");
            foreach (int i in scoreQuery)
            {
                Console.WriteLine($"Score: {i}");
            }

            /*
             * Something quite cool we can do is also say:
             * using (var db = new BloggingContext());
             * as to remove both curley braces denoting the start and end
             * to the code block.
             * This seems to be simply a style choice however.
             */
            using (var db = new BloggingContext()){

                // Create
                Console.WriteLine("Insert a new blog!");
                /*
                 * db.Add acts an INSERT query. 
                 * We are saying below INSERT this new,
                 * Blog Object with the URL:"http://blogs.msdn.com/adonet" 
                 * */
                db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                /*
                 *  SaveChanges();
                 *  Method will automatically call:
                 *  Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges() 
                 *  to discover any changes to entity instances before saving to
                 *  the underlying database. 
                 *  From my understanding this is what commits the change to the DB.
                 *  */
                db.SaveChanges();

                //Read
                Console.WriteLine("Querying for a Blog");
                var blog = db.Blogs
                    .OrderBy(b => b.BlogId)
                    .First();
                /*
                 * I wanted to see what is returned by .First();
                 * 
                 * I did the following: 
                 * var first = blog.First();
                 * Console.WriteLine(first.Url);
                 * 
                 * What we get back is the string from the column URL from the DB.
                 * Okay, lets break down whats happening above.
                 * We are creating a local variable called blog which is of type Blog.
                 * We use Linq queries to order the data and we itterate through the list of BlogIds
                 * We then return the first element.
                 * so following the code so far we should return the first blog which we created above. 
                 */

                //Update:
                Console.WriteLine("Updating the blog and adding a post");
                blog.Url = "https://devblogs.microsoft.com/dotnet";
                blog.Posts.Add(
                    new Post
                    {
                        Title = "Hello World",
                        Content = "I wrote an app using EF Core!"
                    });
                db.SaveChanges();
                /*
                 * What is happening above in this update is really cool! 
                 * So we are still looking at the object blog which 
                 * we first returned from the Querying for a Blog in the above code block.
                 * Then upon this object we have updated the URL, we have then 
                 * created a new Post Object and assigned this Post Object to
                 * the Posts list. We then commit the changes to the Database.
                 */


                //Delete
                /*
                 * As you could imagine we are about to delete a blog.
                 * From my understanding we are involking the .Remove method on
                 * the instance of BloggingContext called db. We can involk this
                 * because we inherit DbContext.
                 * We perform this method on the object 'blog'
                 * which we first queried up on lines 53-55.
                 * We then Save the changes with the SaveChanges() Method 
                 * which commits the change to the database.
                 */

                Console.WriteLine("Delete the blog");
                db.Remove(blog);
                db.SaveChanges();
            }


        }
    }
}
