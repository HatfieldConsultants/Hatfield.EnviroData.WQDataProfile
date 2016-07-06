using Hatfield.EnviroData.Core;
using Hatfield.EnviroData.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hatfield.EnviroData.WQDataProfile.Repositories
{
    public class ResultRepository : Repository<Result>, IResultRepository
    {
        IDbContext _context;

        public ResultRepository(IDbContext dbContext)
            : base(dbContext)
        {

        }

        public IQueryable<Result> GetResultsBySiteAndAnalyte(int siteId, int variableId)
        {
            var results = _dbContext.Query<Result>().Where(x => x.VariableID == variableId && x.FeatureAction.SamplingFeatureID == siteId);

            return results;
        }

    }
}
