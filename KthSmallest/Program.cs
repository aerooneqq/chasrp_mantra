using System;

namespace KthSmallest
{
    class Program
    {
        static void Swap<T>(ref T first, ref T second)
        {
            T temp = first; 
            first = second; 
            second = temp;
        }

        static int FindKthSmallest(int[] nums, int k)
        {
            if (nums is null || k < 0 || k >= nums.Length)
            {
                throw new ArgumentException("Unacceptable params.");
            }

            int left = 0;
            int right = nums.Length - 1;

            while (left <= right)
            {
                int pivot = nums[k];
                int subLeft = left; 
                int subRight = right; 

                while (subLeft <= subRight)
                {
                    while (nums[subLeft] < pivot)
                    {
                        subLeft++;
                    }

                    while (nums[subRight] > pivot)
                    {
                        subRight--;
                    }

                    if (subLeft <= subRight)
                    {
                        Swap(ref nums[subRight], ref nums[subLeft]);
                        subLeft++;
                        subRight--;
                    }
                }

                if (subRight < k)
                {
                    left = subLeft;
                }

                if (subLeft > k)
                {
                    right = subRight;
                }
            }

            return nums[k];
        }

        static void Main(string[] args)
        {
            int[] nums = { 2, 1, 4, 6, 5, 3, 7, 8, 9 };
            int k = 2;

            Console.WriteLine($"The {k + 1}th smallest element is: {FindKthSmallest(nums, k)}");
        }
    }
}
