using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class TargetBOL
    {
        public Guid Id { get; set; } = new Guid();
        public string SalonName { get; set; }
        public Guid SalonId { get; set; }
        public int Year { get; set; }
        public double TotalTakings { get; set; }
        public double RetailMonth { get; set; }
        public double WageBillMonth { get; set; }
        public double ClientVisitsMonth { get; set; }
        public double RebooksMonth { get; set; }
        public double ClientVisitsLastYear { get; set; }
        public double IndividualClientVisitsLastYear { get; set; }
        public double NewClientsMonth { get; set; }
        public double TotalClientsInDatabase { get; set; }
        public double WagePercent { get; set; }
        public double RetailPercent { get; set; }
        public double AverageBill { get; set; }
        public double TotalYearTarget { get; set; }
        public double AverageClientVisitsYear { get; set; }
        public double WeeksBetweenAppointments { get; set; }
    }
}
