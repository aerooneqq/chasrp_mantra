using System;
using System.Linq;

namespace QSort
{
    class QSort
    {
        static unsafe void Swap(int* array, int firstIndex, int secondIndex)
        {
            int temp = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = temp;
        }

        static unsafe int Partition(int* array, int length, int left, int right)
        {
            int pivotIndex = (right - left + 1) / 2;
            int pivot = array[pivotIndex];

            while (left <= right)
            {
                while (array[left] < pivot)
                {
                    left += 1;
                }

                while (array[right] > pivot)
                {
                    right -= 1;
                }

                if (left <= right)
                {
                    Swap(array, left++, right--);
                }
            }

            return pivotIndex;
        }

        static unsafe void Sort(int* array, int length, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(array, length, low, high);

                Sort(array, length, low, pivotIndex - 1);
                Sort(array, length, pivotIndex + 1, high);
            }

        }
         
        static unsafe void Main(string[] args)
        {
            int length = int.Parse(Console.ReadLine());

            int* array = stackalloc int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = int.Parse(Console.ReadLine());
            }

            Sort(array, length, 0, length - 1);

            for (int i = 0; i < length; i++)
            {
                Console.Write($"{array[i]} ");
            }
        }
    }
}
