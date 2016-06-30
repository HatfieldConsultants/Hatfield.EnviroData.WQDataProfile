using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hatfield.EnviroData.DataProfile.WQ.Models;

namespace Hatfield.EnviroData.DataProfile.WQ
{
    public interface IDomainBuilder
    {
        DomainBuildResult Build(WQProfileEntity entity, DbContext dbContext);
    }
}
