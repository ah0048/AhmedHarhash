//5. Call some http API/mock this behavior, limit requests to 10 per minute
using System.Threading.RateLimiting;
namespace ConsoleApp6
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var limiter = new FixedWindowRateLimiter(new FixedWindowRateLimiterOptions
            {       
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 100,
                AutoReplenishment = true
            });

            for (int i = 0; i < 100; i++)
            {

                using var lease = await limiter.AcquireAsync(1);

                if (lease.IsAcquired)
                {
                    Console.WriteLine($"Request {i} at {DateTime.Now:T}");

                    await client.GetAsync("https://example.com/api");
                }
                else
                {
                    Console.WriteLine("Request rejected");
                }

            }
        }
    }
}
