using System.Net.WebSockets;

// 4. Stop a long-running async operation when user clicks Cancel
namespace ConsoleApp5
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var cts = new CancellationTokenSource();

            Console.WriteLine("Press C to cancel...");

            var task = DoAsyncWork(cts.Token);

            if (Console.ReadKey().Key == ConsoleKey.C)
            {
                Console.WriteLine("\ncancel requested");
                cts.Cancel();
            }

            await task;
            Console.WriteLine("done");


        }
        static async Task DoAsyncWork(CancellationToken token)
        {
            try
            {
                for (int i  = 0; i < 100; i++) 
                {
                    Console.WriteLine($"Working... {i}");
                    await Task.Delay(500, token);
                    token.ThrowIfCancellationRequested();
                }
                Console.WriteLine("Completed successfully");
            }
            catch (OperationCanceledException)
            {

                Console.WriteLine("Operation CANCELLED");
            }
        }
}
}
