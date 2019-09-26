using System;

namespace LongestCommonSubsequence
{
    class Program
    {
        static int FindLCSRecursive(string firstStr, string secondStr, int firstIndex, int secondIndex)
        {
            if (firstIndex == 0 || secondIndex == 0)
            {
                return 0;
            }

            if (firstStr[firstIndex - 1] == secondStr[secondIndex - 1])
            {
                return 1 + FindLCSRecursive(firstStr, secondStr, firstIndex - 1, secondIndex - 1);
            }

            return Math.Max(FindLCSRecursive(firstStr, secondStr, firstIndex - 1, secondIndex),
                            FindLCSRecursive(firstStr, secondStr, firstIndex, secondIndex - 1));
        }

        static int FindLCSIterative(string firstStr, string secondStr)
        {
            int[,] weightMatrix = new int[firstStr.Length + 1, secondStr.Length + 1];

            for (int i = 0; i < weightMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < weightMatrix.GetLength(1); j++)
                {
                    if (i == 0 || j == 0)
                    {
                        weightMatrix[i, j] = 0;
                    }
                    else if (firstStr[i - 1] == secondStr[j - 1])
                    {
                        weightMatrix[i, j] = weightMatrix[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        weightMatrix[i, j] = Math.Max(weightMatrix[i - 1, j], weightMatrix[i, j - 1]);
                    }
                }
            }

            for (int i = 0; i < weightMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < weightMatrix.GetLength(1); j++)
                {
                    Console.Write($"{weightMatrix[i, j]} ");
                }

                Console.WriteLine();
            }

            return weightMatrix[firstStr.Length, secondStr.Length];
        }


        static void Main(string[] args)
        {
            string firstString = "abcde";
            string secondString = "basdasdasdd";

            Console.WriteLine(FindLCSRecursive(firstString, secondString, firstString.Length, secondString.Length));
            Console.WriteLine(FindLCSIterative(firstString, secondString));
        }
    }
}
