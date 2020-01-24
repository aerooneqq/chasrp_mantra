using System;

namespace FindPiNumberFromRandomGenerator
{
    /// <summary>
    /// GIVEN: We are given the random generator which generates uniformly distributed numbers from interval (0,1)
    /// TASK: Given this generator we should find the PI.
    ///
    /// SOLUTION: we will generate the int.MaxValue points (point is a pair of x, y which are generated using the
    /// Random class). After we've got the point, we check if this point is in the circle (center = (0, 0), R = 1).
    /// If so, we increment the circleDotsCount. Then, we can get the square of the 1/4 part of the circle, this square
    /// equals circleDotsCount / DOTS_COUNT. Then we can calculate the PI using the formula
    /// PI*R^2 = S => PI = A * 4 / R^2 where A is S / 4.
    ///
    /// SAMPLE OUTPUT:
    /// PI number is 3.141591311964016
    /// </summary>
    class Program
    {
        private const int DOTS_COUNT = int.MaxValue;
        private static readonly Random random;

        
        static Program()
        {
            random = new Random(123);
        }
        
        
        static void Main(string[] args)
        {
            double x;
            double y;
            int circleDotsCount = 0;
            
            for (int i = 0; i < DOTS_COUNT; ++i)
            {
                x = random.NextDouble();
                y = random.NextDouble();

                if (Math.Pow(x, 2) + Math.Pow(y, 2) <= 1)
                    ++circleDotsCount;
            }

            double circleArea = (double)circleDotsCount * 4  / DOTS_COUNT;

            Console.WriteLine($"PI number is {circleArea}");
        }
    }
}