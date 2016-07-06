using Hatfield.EnviroData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hatfield.EnviroData.WQDataProfile
{
    public interface IResultRepository: IRepository<Result>
    {
        IQueryable<Result> GetResultsBySiteAndAnalyte(int siteID, int analyteID);
    }
}
