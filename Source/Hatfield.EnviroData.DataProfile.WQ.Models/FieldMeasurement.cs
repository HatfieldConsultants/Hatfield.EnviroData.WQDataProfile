using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.DataProfile.WQ.Models
{
    public class FieldMeasurement
    {
        public DateTime MeasurementDateTime { get; set; }
        public Site MeasurementSite { get; set; }
        public Person MeasuredBy { get; set; }

        public Analyte Analyte { get; set; }
        public float? Value { get; set; }
    }
}
