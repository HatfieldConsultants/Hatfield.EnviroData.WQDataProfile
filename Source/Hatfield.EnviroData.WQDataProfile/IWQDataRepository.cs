using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hatfield.EnviroData.Core;
using Hatfield.EnviroData.Core.Repositories;

namespace Hatfield.EnviroData.WQDataProfile
{
    public interface IWQDataRepository : IRepository<Hatfield.EnviroData.Core.Action>
    {
        IEnumerable<Hatfield.EnviroData.Core.Action> GetAllWQSampleDataActions();
        IEnumerable<Hatfield.EnviroData.Core.Action> GetAllWQAnalyteDataActions();
        Hatfield.EnviroData.Core.Action GetActionById(int Id);

        int SaveChanges();
    }
}
