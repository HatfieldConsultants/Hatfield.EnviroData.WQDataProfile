using Hatfield.EnviroData.Core;
using Hatfield.EnviroData.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hatfield.EnviroData.WQDataProfile.Repositories
{
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
                public UnitRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
