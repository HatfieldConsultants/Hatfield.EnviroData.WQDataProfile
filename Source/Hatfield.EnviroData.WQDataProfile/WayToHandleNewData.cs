using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hatfield.EnviroData.WQDataProfile
{
    public enum WayToHandleNewData
    {
        CreateInstanceForNewData,
        SetNewDataToBeNull,
        ThrowExceptionForNewData,
        WarningForNewData
    } 
}
