//6. Update int counter to be thread - safe without using synchronization mechanisms
namespace ConsoleApp7
{
    internal class Program
    {
        static int safeCounter = 0;
        static int unsafeCounter = 0;
        static void Main(string[] args)
        {
            int iterations = 100000;

            Parallel.For(0, iterations, i =>
            {
                unsafeCounter++;
            });

            Parallel.For(0, iterations, i =>
            {
                Interlocked.Increment(ref safeCounter);
            });
            Console.WriteLine($"Expected: {iterations}");
            Console.WriteLine($"wrong Counter: {unsafeCounter}");
            Console.WriteLine($"correct Counter: {safeCounter}");
        }
    }
}
