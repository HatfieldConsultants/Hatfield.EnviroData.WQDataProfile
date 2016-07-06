using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.DataProfile.WQ
{
    public class DomainBuildResult
    {
        public bool IsValid { get; set; }
        public System.Data.Entity.EntityState State { get; set; }
        public object Data { get; set; }

        public DomainBuildResult(bool isValid, object data, System.Data.Entity.EntityState state)
        {
            IsValid = isValid;
            Data = data;
            State = state;
        }
    }
}
