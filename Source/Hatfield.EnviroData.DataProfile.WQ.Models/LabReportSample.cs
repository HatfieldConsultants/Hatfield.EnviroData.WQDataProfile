using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.DataProfile.WQ.Models
{
    public class LabReportSample : WQProfileEntity
    {
        public long Id { get; set; }
        public Site Site { get; set; }
        public Lab Lab { get; set; }        
        public Analyte Analyte { get; set; }

        public Person ImportBy { get; set; }
        public Person ValidateBy { get; set; }

        public DateTime? DateTime { get; set; }
        public long UTCOffset { get; set; }
        public Unit Unit { get; set; }

        public float? Value { get; set; }
        public ProcessingLevel ProcessingLevel { get; set; }
    }
}
