using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hatfield.EnviroData.Core;

namespace Hatfield.EnviroData.WQDataProfile
{
    public interface IDataVersioningHelper
    {
        Hatfield.EnviroData.Core.Action CloneActionData(Hatfield.EnviroData.Core.Action previousAction);
    }
}
