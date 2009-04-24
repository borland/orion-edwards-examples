using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FuncDemo1
{
    class Program
    {
        private static readonly List<int> l = new List<int>();

        static void Main(string[] args)
        {
            var r = new FunctionalPersonRepository();
            //Console.WriteLine("The Oldest person is {0}", r.FindOldest().Name);
            //Console.WriteLine();

            var matchingPeople = r.FindMatchingNames("o", (haystack, needle) => haystack.Contains(needle));

            var strings = matchingPeople.Select(delegate(Person p) { return p.Name; });

            Console.WriteLine("People with names matching 'o' are: {0}", String.Join(",", strings.ToArray()));
            Console.WriteLine();

            //Console.WriteLine("Sum of ages is {0}", r.SumAges());
            //Console.WriteLine();

            //Console.WriteLine("=== Dumping People ===");
            //foreach( var n in r.GetAllNames() )
            //    Console.WriteLine(n);
        }


        public static Func<string, bool> BuildFilter(int whichCheckbox)
        {
            Console.WriteLine("This is a side effect");

            if( whichCheckbox == 0 )
                return s => s.Contains("o");
            else
                return s => Regex.IsMatch(s, "^L");
        }

        public static List<Func<int>> Counter(int stopAt)
        {
            var l = new List<Func<int>>();
            for (int i = 0; i < stopAt; i++)
            {
                l.Add(() => { return i; });
            }

            return l;
        }


    }
}
