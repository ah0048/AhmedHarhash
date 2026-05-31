namespace ConsoleApp1
{
    enum MusicType {
        Hardbass,
        Latino,
        Rock
    }
internal class Program
    {
        static MusicType currentTrack;
        static bool isFinished = false;
        static object locker = new object();
        static void Main(string[] args)
        {
            Random random = new Random();
            MusicType[] tracks = new MusicType[20];
            for (int i = 0; i < tracks.Length; i++)
            {
                tracks[i] = (MusicType)random.Next(0, 3);
            }

            Thread[] dancers = new Thread[10];
            for (int i = 0;i < dancers.Length; i++)
            {
                dancers[i] = new Thread(dance);
                dancers[i].Name = $"Dancer {i + 1}";
                dancers[i].Start();
                Thread.Sleep(500); // wait for all dancers to start
            }

            foreach (var track in tracks)
            {
                lock (locker)
                {
                    currentTrack = track;
                    Console.WriteLine($"Now playing: {currentTrack}");
                    Monitor.PulseAll(locker);
                }
                Random rnd = new Random();
                Thread.Sleep(rnd.Next(10000, 20001));  // Simulate track duration 10 to 20 secs
            }

            lock (locker) { 
                isFinished = true;
                Monitor.PulseAll(locker);
            }
            Console.ReadLine();

        }

        static void dance()
        {
            while (true) { 

                lock(locker) 
                {

                    Monitor.Wait(locker);
                    if (isFinished)
                        break;

                    switch (currentTrack)
                    {
                        case MusicType.Hardbass:
                            Console.WriteLine($"{Thread.CurrentThread.Name}: Elbow dance for Hardbass");
                            break;
                        case MusicType.Latino:
                            Console.WriteLine($"{Thread.CurrentThread.Name}: Hips dance for Latino");
                            break;
                        case MusicType.Rock:
                            Console.WriteLine($"{Thread.CurrentThread.Name}: Head dance for Rock");
                            break;
                    }
                }
            }
        }
    }
}
