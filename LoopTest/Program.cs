using System;
using System.Diagnostics;
using Infrastructure;

namespace LoopTest
{
    class Program
    {
        /*
         * Sample output: 
         * 
         * i++ loop time: 00:00:00.0004222
         * ++i loop time: 00:00:00.0003858
         */
        static void Main(string[] args)
        {
            TestPostfixIncrement();
            TestPrefixIncrement();
        }

        static void TestPostfixIncrement() 
        {
            TimeSpan overallTime = new TimeSpan();

            for (int j = 0; j < 10000; j++)
            {
                var stopwatch = Stopwatch.StartNew();

                for (int i = 0; i < 10000; i++) { }

                overallTime += stopwatch.Elapsed;
            }

            Console.WriteLine($"i++ loop time: {overallTime / 1000}");
        }

        static void TestPrefixIncrement()
        {
            TimeSpan overallTime = new TimeSpan();

            for (int j = 0; j < 10000; j++)
            {
                var stopwatch = Stopwatch.StartNew();

                for (int i = 0; i < 10000; ++i) { }

                overallTime += stopwatch.Elapsed;
            }

            Console.WriteLine($"++i loop time: {overallTime / 1000}");
        }
    }
}
