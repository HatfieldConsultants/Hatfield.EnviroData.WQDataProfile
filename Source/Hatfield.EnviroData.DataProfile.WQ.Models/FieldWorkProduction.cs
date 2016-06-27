using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.DataProfile.WQ.Models
{
    public class FieldWorkProduction
    {
        public SamplingActivity SamplingActivity { get; set; }
        public IEnumerable<WaterQualitySample> Samples { get; set; }
    }
}
