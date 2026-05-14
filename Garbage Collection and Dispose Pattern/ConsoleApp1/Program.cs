namespace ConsoleApp1
{
    public class Customer : IDisposable
    {

        private StringReader _reader;
        bool disposed = false;
        public Customer(string s)

        {

            this._reader = new StringReader(s);

        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                _reader?.Dispose();
            }

            disposed = true;
        }

        ~Customer()
        {
            Dispose(false);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("playing with GC and Dispose pattern");
        }
    }
}
