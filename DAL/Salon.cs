using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Salon
    {
        public Guid SalonId { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }

        public ICollection<Actual> Actuals { get; set; }
        public ICollection<Target> Targets { get; set; }
    }
}
