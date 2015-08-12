using Hatfield.EnviroData.Core;
using Hatfield.EnviroData.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hatfield.EnviroData.WQDataProfile.Repositories
{
    public class WQVariableRepository : Repository<Variable>, IWQVariableRepository
    {

        public WQVariableRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }


        public IQueryable<Variable> GetAllChemistryVariables()
        {
            var variables = _dbContext.Query<Variable>().Where(x => x.CV_VariableType.Name == "Chemistry");

            return variables;
        }
    }
}
