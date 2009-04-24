using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FuncDemo1
{
    class FunctionalPersonRepository
    {
        public List<Family> families = new List<Family>();

        public FunctionalPersonRepository()
        {
            var orion = new Person();
            orion.Name = "Orion";
            orion.Age = 27;

            var liam = new Person();
            liam.Name = "Liam";
            liam.Age = 21;

            var geoff = new Person();
            geoff.Name = "Geoff";
            geoff.Age = 30;

            var edwardses = new Family();
            edwardses.Surname = "Edwards";
            edwardses.Members.Add(orion);
            edwardses.Members.Add(liam);

            var thornburrows = new Family();
            thornburrows.Members.Add(geoff);

            families.Add(edwardses);
            families.Add(thornburrows);
        }

        public Person FindOldest()
        {
            Person oldest = null;
            foreach (var f in families)
            {
                foreach (var p in f.Members)
                {
                    if (oldest == null)
                        oldest = p;

                    if (p.Age > oldest.Age)
                        oldest = p;
                }
            }
            return oldest;
        }

        public IEnumerable<Person> FindMatchingNames(string partialName, Func<string, string, bool> func)
        {
            return AllPeople().Where(p => func(p.Name, partialName));
        }

        public IEnumerable<Person> AllPeople()
        {
            return from f in families
                   from p in f.Members
                   select p;
        }

        public int SumAges()
        {
            int sum = 0;
            foreach (var f in families)
                foreach (var p in f.Members)
                    sum += p.Age;

            return sum;
        }

        public List<string> GetAllNames()
        {
            var ret = new List<string>();

            foreach (var f in families)
                foreach (var p in f.Members)
                    ret.Add(p.Name + " " + f.Surname);

            return ret;
        }
    }
}
