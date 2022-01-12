using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class SalonBOL
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ContactName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
