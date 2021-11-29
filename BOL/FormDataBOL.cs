using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class FormDataBOL
    {
        public Guid Id { get; set; } = new Guid();
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string SalonName { get; set; }
        public string Email { get; set; }
        public string Month { get; set; }
        public double TotalMonthlyTakings { get; set; }
        public double RetailMonthUSD { get; set; }
        public double WageBillMonthUSD { get; set; }
        public int ClientVisitsMonth { get; set; }
        public double TargetMonthUSD { get; set; }
        public int TargetClientsMonth { get; set; }
        public double RebooksMonth { get; set; }
        public int TotalClientVisitsYear { get; set; }
        public int TotalIndividualClientVisitsYear { get; set; }
        public int NewClientsMonth { get; set; }
        public double PastYearTotalTakings { get; set; }
        public int TotalClientsInDatabase { get; set; }
    }
}
