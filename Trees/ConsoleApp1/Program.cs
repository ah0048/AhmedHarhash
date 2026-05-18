using System.Reflection.Metadata.Ecma335;
using System.Windows.Markup;

namespace ConsoleApp1
{
    class Node
    {
        public int Value { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node(int val)
        {
            this.Value = val;
        }
    }

    class Solution
    {
        public string Serialize(Node root)
        {
            List<string> res = new List<string>();
            void DFS(Node? node)
            {
                if (node == null) {
                    res.Add("NIL");
                    return;
                }
                res.Add(node.Value.ToString());
                DFS(node.Left);
                DFS(node.Right);
            }
            DFS(root);
            return string.Join(",", res);
        }

        public Node? Deserialize(string data)
        {
            List<string> vals = data.Split(',').ToList();
            int i = 0;
            Node? DFS()
            {
                if (vals[i] == "NIL")
                {
                    i++;
                    return null;
                }
                Node node = new Node(int.Parse(vals[i]));
                i++;
                node.Left = DFS();
                node.Right = DFS();
                return node;
            }
            return DFS();

        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //      10
            //     /  \
            //    5    20
            //   / \   /
            //  3   7 15


            Node root = new Node(10);
            root.Left = new Node(5);
            root.Right = new Node(20);

            root.Left.Left = new Node(3);
            root.Left.Right = new Node(7);

            root.Right.Left = new Node(15);



            Solution sol = new Solution();

            // Serialize
            string serialized = sol.Serialize(root);
            Console.WriteLine("Serialized: ");
            Console.WriteLine(serialized);

            // Deserialize
            Node? newRoot = sol.Deserialize(serialized);

            // Serialize again to verify correctness
            string reSerialized = sol.Serialize(newRoot);
            Console.WriteLine("\nRe-Serialized (after deserialization):");
            Console.WriteLine(reSerialized);

            // Check if both match
            Console.WriteLine("\nSame result? " + (serialized == reSerialized));


        }
    }
}
