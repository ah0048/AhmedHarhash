using System.Collections.Concurrent;
// 2. Given a list of 1,000,000 integers:
// - calculate the square of each number,
// - store the result in a thread-safe collection.
namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {


            var numbers = new List<int>();
            for (int i = 0; i < 1_000_000; i++)
            {
                numbers.Add(i);
            }


            ConcurrentDictionary<int, long> results = new ConcurrentDictionary<int, long>();

            Parallel.ForEach(numbers, n =>
            {
                results[n] = (long)n * n;
            });

            Console.WriteLine($"Total results: {results.Count}");
            Console.WriteLine($"square of 5: {results[5]}");

        }
    }
}
