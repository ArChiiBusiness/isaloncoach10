using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class StatisticsBOL
    {
        public int Customers { get; set; } = 0;
        public int Responses { get; set; } = 0;
        public DateTime? LastResponse { get; set; }
    }
}
