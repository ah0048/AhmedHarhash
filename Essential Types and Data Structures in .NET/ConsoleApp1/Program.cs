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
        // if these are the requiremnets a dynamic circular array will be used

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

    public class Person
    {
        public string? Name { get;}
        public int Age { get;}
        public string? Job { get; }
        public double Salary { get;}
        public string? Address { get;}


        public Person(string name, int age, string job, double salary, string address)
        {
            Name = name;
            Age = age;
            Job = job;
            Salary = salary;
            Address = address;
        }


        public override int GetHashCode()
        {
            unchecked 
            {
                int hash = 17;
                
                if (Name != null)
                    hash = hash * 23 + Name.GetHashCode();

                hash = hash * 23 + Age.GetHashCode();

                if (Job != null)
                    hash = hash * 23 + Job.GetHashCode();

                hash = hash * 23 + Salary.GetHashCode();

                if (Address != null)
                    hash = hash * 23 + Address.GetHashCode();

                return hash;
            }
        }
        public override bool Equals(object? obj)
        {
            Person other = obj as Person;

            if (other == null) return false;

            if (this.Name == other.Name && this.Age == other.Age &&
                this.Job == other.Job && this.Salary == other.Salary &&
                this.Address == other.Address)
            {
                return true;
            }
            return false;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================= Task 1 =================================");

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

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("================================= Task 2 =================================");

                Console.WriteLine("=== Create objects ===");

                var p1 = new Person("Ahmed", 25, "Dev", 1000, "Cairo");
                var p2 = new Person("Ahmed", 25, "Dev", 1000, "Cairo"); // same content
                var p3 = new Person("Ali", 30, "Manager", 2000, "Giza"); // different

                Console.WriteLine($"p1.Equals(p2): {p1.Equals(p2)}"); // true
                Console.WriteLine($"p1 == p2: {p1 == p2}");           // false (different refs)

                Console.WriteLine("\n=== Dictionary Test ===");

                var dict = new Dictionary<Person, string>();

                dict[p1] = "First person";

                Console.WriteLine(dict[p2]); // should work (same hash code)

                Console.WriteLine("\n=== HashSet Test ===");

                var set = new HashSet<Person>();

                set.Add(p1);
                set.Add(p2); // should not add (duplicate hash code)

                Console.WriteLine($"HashSet count: {set.Count}"); //  1

                Console.WriteLine("\n=== Hashtable Test ===");

                var hashtable = new Hashtable();

                hashtable[p1] = "Stored in hashtable";

                Console.WriteLine(hashtable[p2]); // should work

        }
    }
}
