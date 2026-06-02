using System.Collections.Concurrent;
// 1. Using Dictionary as cache build concurrent CRUD operations for it
namespace ConsoleApp2
{
    #region sol 1 
    //internal class Program
    //{
    //    static Dictionary<int, string> cache = new Dictionary<int, string>();
    //    static object locker = new object();
    //    static void Main(string[] args)
    //    {
    //        Thread t1 = new Thread(Add);
    //        Thread t2 = new Thread(Read);
    //        Thread t3 = new Thread(Update);
    //        Thread t4 = new Thread(Delete);

    //        t1.Start();
    //        t2.Start();
    //        t3.Start();
    //        t4.Start();

    //        t1.Join();
    //        t2.Join();
    //        t3.Join();
    //        t4.Join();

    //        Console.WriteLine("Finished");

    //    }

    //    static void Add()
    //    {
    //        for (int i = 0; i < 10; i++)
    //        {
    //            lock (locker)
    //            {

    //                cache[i] = $"Value {i}";
    //                Console.WriteLine($"[ADD] Key={i}");

    //            }
    //            Thread.Sleep(100);
    //        }

    //    }

    //    static void Read()
    //    {
    //        for (int i = 0; i < 10; i++)
    //        {
    //            lock (locker)
    //            {
    //                if (cache.ContainsKey(i))
    //                    Console.WriteLine($"[READ] Key={i}, Value={cache[i]}");
    //                else
    //                    Console.WriteLine($"[READ] Key={i} NOT FOUND");
    //            }
    //            Thread.Sleep(150);
    //        }

    //    }
    //    static void Update()
    //    {
    //        for (int i = 0; i < 10; i++)
    //        {
    //            lock (locker)
    //            {
    //                if (cache.ContainsKey(i))
    //                {

    //                    cache[i] = $"Updated {i}";
    //                    Console.WriteLine($"[UPDATE] Key={i}");

    //                }
    //            }
    //            Thread.Sleep(200);
    //        }
    //    }



    //    static void Delete()
    //    {
    //        for (int i = 0; i < 10; i++)
    //        {
    //            lock (locker)
    //            {
    //                if (cache.ContainsKey(i))
    //                {
    //                    cache.Remove(i);
    //                    Console.WriteLine($"[DELETE] Key={i}");
    //                }
    //            }
    //            Thread.Sleep(250);
    //        }


    //    }

    //}
    #endregion

    #region sol 2
    internal class Program
    {
        static ConcurrentDictionary<int, string> cache = new ConcurrentDictionary<int, string>();

        static void Main()
        {
            Parallel.Invoke(
                Add,
                Read,
                Update,
                Delete
            );

            Console.WriteLine("Finished");
        }


        static void Add()
        {
            for (int i = 0; i < 10; i++)
            {
                if (cache.TryAdd(i, $"Value {i}"))
                {
                    Console.WriteLine($"[ADD] Key={i}");
                }

                Task.Delay(100).Wait();
            }
        }


        static void Read()
        {
            for (int i = 0; i < 10; i++)
            {
                if (cache.TryGetValue(i, out var value))
                {
                    Console.WriteLine($"[READ] Key={i}, Value={value}");
                }
                else
                {
                    Console.WriteLine($"[READ] Key={i} NOT FOUND");
                }

                Task.Delay(150).Wait();
            }
        }


        static void Update()
        {
            for (int i = 0; i < 10; i++)
            {
                cache.AddOrUpdate(
                    i,
                    "New Value", // if not exists
                    (key, oldValue) => $"Updated {key}"
                );

                Console.WriteLine($"[UPDATE] Key={i}");

                Task.Delay(200).Wait();
            }
        }


        static void Delete()
        {
            for (int i = 0; i < 10; i++)
            {
                if (cache.TryRemove(i, out _))
                {
                    Console.WriteLine($"[DELETE] Key={i}");
                }

                Task.Delay(250).Wait();
            }
        }
    }
    #endregion
}
