/*
WRITTEN BY M. EBRAHIM SOLOMON. 01/09/24
AAT ASSESSMENT.
COPYRIGHT.
RESOURCES - MICROSOFT DOCUMENTATION:
https://learn.microsoft.com/en-us/dotnet/api/system.threading.semaphore?view=netframework-4.8
*/

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AAT_Assessment
{
    public class Program
    {
        static ConcurrentBag<int> globalList = new ConcurrentBag<int>();
        static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        static CancellationTokenSource cts = new CancellationTokenSource();

        static async Task Main(string[] args)
        {
            var oddTask = Task.Run(() => NumberGenerators.GenerateOddNumbers(globalList, semaphore, cts.Token));
            var primeTask = Task.Run(() => NumberGenerators.GeneratePrimeNumbers(globalList, semaphore, cts.Token));

            var evenTask = Task.Run(() =>
            {
                while (globalList.Count < 250000)
                {
                    Thread.Sleep(100); 
                }
                NumberGenerators.GenerateEvenNumbers(globalList, semaphore, cts.Token);
            });

            var cancelTask = Task.Run(() =>
            {
                while (globalList.Count < 1000000)
                {
                    Thread.Sleep(100); 
                }
                cts.Cancel();
            });

            await Task.WhenAll(oddTask, primeTask, evenTask, cancelTask);

            var sortedList = globalList.OrderBy(x => x).ToList();
            int oddCount = sortedList.Count(x => x % 2 != 0);
            int evenCount = sortedList.Count(x => x % 2 == 0);

            Console.WriteLine($"Odd Count: {oddCount}");
            Console.WriteLine($"Even Count: {evenCount}");

            Console.ReadKey();
            Utils.SerializeToBinary(sortedList, "list.bin");
            Utils.SerializeToXml(sortedList, "list.xml");
        } 
    }
}
