//3. Run a quick background fire-and-forget operation
namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Task.Run(() =>
            {
                Console.WriteLine("Background work started");

                Task.Delay(2000).Wait();

                Console.WriteLine("Background work finished");
            });

            Console.WriteLine("Main finished");

        }
    }
}
