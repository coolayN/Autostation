using Autostation.Cafe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autostation
{
    class Gas:Good
    {
        public double sum { get; set; }

        public Gas(string name, double price)
            :base(name, price)
        {
        }

        public string NumOfLitres()
        {
            if (sum != 0) return (sum / price).ToString("0.00");
            else return "0";
        }

        public string SumToPayForGas()
        {
            if (num != 0) return (num * price).ToString("0.00");
            else return "0";
        }
    }
}
