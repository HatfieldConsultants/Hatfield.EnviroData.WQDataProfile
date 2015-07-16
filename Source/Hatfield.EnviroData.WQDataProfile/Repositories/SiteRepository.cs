using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hatfield.EnviroData.Core;
using Hatfield.EnviroData.Core.Repositories;

namespace Hatfield.EnviroData.WQDataProfile.Repositories
{
    public class SiteRepository : Repository<Site>, ISiteRepository
    {
        public SiteRepository(IDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
