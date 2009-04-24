using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuncDemo1
{
    class ImperativePersonRepository
    {
        private List<Family> families = new List<Family>();

        public ImperativePersonRepository()
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

        public List<Person> FindMatchingNames(string partialName)
        {
            var ret = new List<Person>();
            foreach (var f in families)
            {
                foreach (var p in f.Members)
                {
                    if (p.Name.IndexOf(partialName, StringComparison.InvariantCultureIgnoreCase) != -1)
                        ret.Add(p);
                }
            }
            return ret;
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

            foreach( var f in families )
                foreach( var p in f.Members )
                    ret.Add(p.Name + " " + f.Surname);

            return ret;
        }
    }
}
