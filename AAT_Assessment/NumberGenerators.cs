using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AAT_Assessment
{
    public static class NumberGenerators
    {
        public static void GenerateOddNumbers(ConcurrentBag<int> globalList, SemaphoreSlim semaphore, CancellationToken token)
        {
            Random random = new Random();
            while (!token.IsCancellationRequested)
            {
                if (globalList.Count >= 1000000)
                    break;

                int oddNumber = random.Next(1, 1000000) * 2 + 1;

                semaphore.Wait();
                if (globalList.Count < 1000000) 
                {
                    globalList.Add(oddNumber);
                }
                semaphore.Release();
            }
        }

        public static void GeneratePrimeNumbers(ConcurrentBag<int> globalList, SemaphoreSlim semaphore, CancellationToken token)
        {
            int number = 2;
            while (!token.IsCancellationRequested)
            {
                if (globalList.Count >= 1000000)
                    break;

                if (IsPrime(number))
                {
                    semaphore.Wait();
                    if (globalList.Count < 1000000) 
                    {
                        globalList.Add(-number);
                    }
                    semaphore.Release();
                }
                number++;
            }
        }

        public static void GenerateEvenNumbers(ConcurrentBag<int> globalList, SemaphoreSlim semaphore, CancellationToken token)
        {
            int number = 2;
            while (!token.IsCancellationRequested)
            {
                if (globalList.Count >= 1000000)
                    break;

                semaphore.Wait();
                if (globalList.Count < 1000000) 
                {
                    globalList.Add(number);
                    number += 2;
                }
                semaphore.Release();
            }
        }

        private static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
