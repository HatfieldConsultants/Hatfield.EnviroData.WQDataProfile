using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.DataProfile.WQ.Models
{
    public class Site
    {
        public int Id { get; protected set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
    }
}
