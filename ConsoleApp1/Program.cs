using System;

namespace ConsoleApp1
{
    class A
    {
        public int Add(int x, int y)
        {
            Console.WriteLine($"{x}, {y}");
            return x + y;
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            int x = 0;
            int y = 0;
            Add(x, y);
        }

        public static void Add(int x, int y)
        {
            A a = new A();
            a.Add(x, y);
        }
    }
}
