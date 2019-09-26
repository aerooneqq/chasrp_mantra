using System;

namespace LongestCommonSubstring
{
    class Program
    {
        static int GetLCSLength(string firstStr, string secondStr)
        {
            int[,] matrix = new int[firstStr.Length + 1, secondStr.Length + 1];
            int maxLength = int.MinValue;

            for (int i = 1; i < firstStr.Length; i++)
            {
                for (int j = 1; j < secondStr.Length; j++)
                {
                    if (firstStr[i - 1] == secondStr[j - 1])
                    {
                        matrix[i, j] = matrix[i - 1, j - 1] + 1;

                        if (matrix[i, j] > maxLength)
                        {
                            maxLength = matrix[i, j];
                        }
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }
                }
            }

            for (int i = 0; i < firstStr.Length; i++)
            {
                for (int j = 0; j < secondStr.Length; j++)
                {
                    Console.Write($"{matrix[i, j]} ");
                }

                Console.WriteLine();
            }

            return maxLength;
        }

        static void Main(string[] args)
        {
            string firstStr = "qwertyu";
            string secondStr = "asdasdasdasdasdqwerasdasdasdl;askd ;lasd kals;";

            Console.WriteLine(GetLCSLength(firstStr, secondStr));
        }
    }
}
