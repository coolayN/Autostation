using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autostation.Cafe
{
    class Good
    {
        public string name { get; private set; }
        public double price { get; private set; }
        public double num { get;  set; }

        public Good(string name, double price)
        {
            this.name = name;
            this.price = price;
        }
    }
}
