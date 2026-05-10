using System.Collections;

namespace ConsoleApp1
{

    public interface IQueue
    {
        void Enqueue(int element);
        int Dequeue();
        int Peek();
    }



    public class QueueLike : IQueue
    {
        // built-in linked list is used here cuz it's easier to implement 
        // linked list will take more space (node = value + next & prev pointers)
        // array will take less space
        // performace of array is better in this case in caching and low memory overhead (contiguous memory)
        // will have to use array when the priority is to optimize performance and reduce used space
        // if these are the requiremnets a dynamic circular array will used 
        private readonly LinkedList<int> list = new LinkedList<int>();
        public int Count { 
            get
            {
                return list.Count;
            }
        }
        public int Dequeue()
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }
            int value = list.First.Value;
            list.RemoveFirst();
            return value;
        }

        public void Enqueue(int element)
        {
            list.AddLast(element);
        }

        public int Peek()
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            return list.First.Value;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================= First Task =================================");

            IQueue queue = new QueueLike();

            Console.WriteLine("=== Enqueue Test ===");
            queue.Enqueue(10);
            queue.Enqueue(20);
            queue.Enqueue(30);

            Console.WriteLine(queue.Peek());   // Expected: 10

            Console.WriteLine("\n=== Dequeue Test ===");
            Console.WriteLine(queue.Dequeue()); // Expected: 10
            Console.WriteLine(queue.Dequeue()); // Expected: 20

            Console.WriteLine("\n=== Peek Test ===");
            Console.WriteLine(queue.Peek()); // Expected: 30

            Console.WriteLine("\n=== Enqueue After Dequeue ===");
            queue.Enqueue(40);
            queue.Enqueue(50);

            Console.WriteLine(queue.Dequeue()); // Expected: 30
            Console.WriteLine(queue.Dequeue()); // Expected: 40
            Console.WriteLine(queue.Dequeue()); // Expected: 50

            Console.WriteLine("\n=== Empty Queue Test ===");

            try
            {
                queue.Dequeue(); // should throw
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Expected: Queue is empty
            }

            try
            {
                queue.Peek(); // should throw
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Expected: Queue is empty
            }
        }
    }
}
