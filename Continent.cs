using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App2
{
    public class Continent
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Continent() { }
        public Continent(string Code, string Name)
        {
            this.Code = Code;
            this.Name = Name;
        }
    }
}
