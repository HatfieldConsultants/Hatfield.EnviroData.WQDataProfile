using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.DataProfile.WQ.Models
{
    public class WaterQualityObservation : WQProfileEntity
    {
        public long Id { get; protected set; }
        public Lazy<Site> Site { get; set; }
        public Lazy<Lab> Lab { get; set; }        
        public Lazy<Analyte> Analyte { get; set; }

        public Lazy<Person> ImportBy { get; set; }
        public Lazy<Person> ValidateBy { get; set; }

        public DateTime? DateTime { get; set; }
        public long UTCOffset { get; set; }
        public Lazy<Unit> Unit { get; set; }

        public float? Value { get; set; }
        public ProcessingLevel ProcessingLevel { get; set; }
    }
}
