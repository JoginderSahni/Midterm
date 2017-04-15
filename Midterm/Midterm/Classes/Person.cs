using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm
{
    class Person
    {
        public Person()
        { }
        public Person (string first, string second, double num)
        {
            this.FirstName = first;
            this.LastName = second;
            this.PhoneNumber = num;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double PhoneNumber { get; set; }
    }
}
