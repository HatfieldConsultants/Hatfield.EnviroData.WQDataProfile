using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hatfield.EnviroData.Core;

namespace Hatfield.EnviroData.DataProfile.WQ
{
    public class ProcessingLevelHelper
    {
        public static ProcessingLevel GetOdm2ProcessingLevel(Hatfield.EnviroData.DataProfile.WQ.Models.ProcessingLevel level, ODM2Entities dbContext)
        {
            var foundLevel = dbContext.ProcessingLevels.FirstOrDefault(x => x.ProcessingLevelCode == level.ToString());

            if(foundLevel == null)
            {
                throw new KeyNotFoundException(string.Format("System can not find matched processing level for {0}", level));
            }

            return foundLevel;
        }
    }
}
