using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace RxConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Enumerable_Sample();
            Observable_Sample();

            var last = "_";
            while (last != "quit" && last != "")
                Console.WriteLine(">> " + (last = Console.ReadLine()));
        }

        static void Enumerable_Sample()
        {
            var q = GetFilesRecursive("c:\\windows")
                .Where(f => f.EndsWith(".exe"))
                .Take(10);

            foreach (var f in q)
            {
                Console.WriteLine(f);
            }

            Console.WriteLine("ALL FINISHED");
        }

        static void Observable_Sample()
        {
            var q = GetFilesRecursiveO("c:\\windows")
                .Where(f => f.EndsWith(".exe"))
                .Take(10);

            q.Subscribe(
                    f => Console.WriteLine(f),
                    () => Console.WriteLine("ALL FINISHED")
                );
        }

        static IEnumerable<string> GetFilesRecursive(string path)
        {
            return Directory.GetFiles(path).SleepEvery(1)
                .Concat(Directory.GetDirectories(path).SelectMany(d => GetFilesRecursive(d)));
        }

        static IObservable<string> GetFilesRecursiveO(string path)
        {
            return Directory.GetFiles(path).SleepEvery(1).ToObservable()
                .Concat(Directory.GetDirectories(path).ToObservable().SelectMany(d => GetFilesRecursiveO(d)));
        }
    }

    static class Ext
    {
        public static IEnumerable<T> SleepEvery<T>(this IEnumerable<T> source, int sleepTime)
        {
            return source.Select(x => { Thread.Sleep(sleepTime); return x; });
        }
    }
}
