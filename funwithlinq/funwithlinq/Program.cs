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


            // Define the query expression. Lets just itterate over an array of ints for this example.
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

        }
    }
}
