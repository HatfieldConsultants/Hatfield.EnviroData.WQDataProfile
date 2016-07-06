using Hatfield.EnviroData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hatfield.EnviroData.WQDataProfile
{
    public interface IWQVariableRepository: IVariableRepository, IRepository<Variable>
    {
        IQueryable<Variable> GetAllChemistryVariables();
    }
}
