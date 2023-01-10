using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    public class NeedRow
    {
        public string Type { get; set; }

        public double Amount { get; set; }

        public NeedRow(string type, double amount)
        {
            Type = type;
            Amount = amount;
        }

        public void Add(int count)
        {
            Amount += count;
        }

        public void Subtract(int count)
        {
            Amount -= count;
        }
    }
}
