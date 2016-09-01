using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.DataProfile.WQ.Models
{
    public class FieldTripPlan
    {
        public Project Project { get; set; }
        public IEnumerable<Person> FieldCrews { get; set; }
        public IEnumerable<Site> Locations { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public IEnumerable<Lab> Labs { get; set; }
    }
}
