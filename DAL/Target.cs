using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Target
    {
        public Guid TargetId { get; set; } = new Guid();
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

        public Guid SalonId { get; set; }
        public Salon Salon { get; set; }
    }
}
