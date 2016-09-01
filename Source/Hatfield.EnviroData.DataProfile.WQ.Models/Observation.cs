using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.DataProfile.WQ.Models
{
    public class Observation
    {
        public Project Project { get; set; }
        public FieldVisit SamplingActivity { get; set; }
        public IEnumerable<FieldMeasurement> FieldMeasurements { get; set; }
        public IEnumerable<FieldSpecimen> Specimens { get; set; }
        public IEnumerable<LabReportSample> Samples { get; set; }
    }
}
