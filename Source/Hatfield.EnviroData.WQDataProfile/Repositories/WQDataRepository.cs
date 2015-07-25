using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hatfield.EnviroData.Core;
using Hatfield.EnviroData.Core.Repositories;

namespace Hatfield.EnviroData.WQDataProfile.Repositories
{
    public class WQDataRepository : Repository<Core.Action>, IWQDataRepository
    {
        public WQDataRepository(IDbContext dbContext)
            : base(dbContext)
        {

        }

        public IEnumerable<Core.Action> GetAllWQAnalyteDataActions()
        {
            throw new NotImplementedException("Get all WQ analyte data actions function is not implemented");
        }

        public IEnumerable<Core.Action> GetAllWQSampleDataActions()
        {
            throw new NotImplementedException("Get all WQ sample data actions function is not implemented");
        }


        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
