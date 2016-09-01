using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.DataProfile.WQ.Models
{
    public class FieldVisit
    {
        public long Id { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public long StartDateTimeUTCOffset { get; set; }
        public long EndDateTimeUTCOffset { get; set; }
        public Site Site { get; set; }


    }
}
