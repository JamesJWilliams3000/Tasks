using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    public class TotalsRow
    {
        public string Type { get; set; }

        public int Sum { get; set; }

        public double Percent { get; set; }

        public TotalsRow(string type, int sum, int total)
        {
            Type = type;
            Sum = sum;

            Percent = (double)sum / total;
        }
    }
}
