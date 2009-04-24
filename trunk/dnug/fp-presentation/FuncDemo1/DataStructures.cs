using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuncDemo1
{
    class Family
    {
        static int lastId = 1;

        public Family()
        {
            Id = lastId++;
            Members = new List<Person>();
        }

        public int Id { get; private set; }
        public string Surname { get; set; }
        public List<Person> Members { get; private set; }
    }

    class Person
    {
        static int lastId = 1;

        public Person()
	    {
            Id = lastId++;
	    }

        public int Id { get; private set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
