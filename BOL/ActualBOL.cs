using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class ActualBOL
    {
        public Guid Id { get; set; } = new Guid();
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string SalonName { get; set; }
        public Guid SalonId { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double TotalTakings { get; set; }
        public double RetailMonth { get; set; }
        public double WageBillMonth { get; set; }
        public double ClientVisitsMonth { get; set; }
        public double RebooksMonth { get; set; }
        public double ClientVisitsLastYear { get; set; }
        public double IndividualClientVisitsLastYear { get; set; }
        public double NewClientsMonth { get; set; }
        public double WagePercent { get; set; }
        public double RetailPercent { get; set; }
        public double AverageBill { get; set; }
        public double TotalTakingsYear { get; set; }
        public double AverageClientVisitsYear { get; set; }
        public double WeeksBetweenAppointments { get; set; }
    }
}
