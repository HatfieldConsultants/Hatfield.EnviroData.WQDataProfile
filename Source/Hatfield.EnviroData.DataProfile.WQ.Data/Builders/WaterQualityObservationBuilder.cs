using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hatfield.EnviroData.Core;
using Hatfield.EnviroData.DataProfile.WQ.Models;

namespace Hatfield.EnviroData.DataProfile.WQ.Builders
{
    public class WaterQualityObservationBuilder : IDomainBuilder
    {
        public DomainBuildResult Build(Models.WQProfileEntity entity, Core.ODM2Entities dbContext)
        {
            throw new NotImplementedException();
        }

        public bool IsDataDirty(Models.WQProfileEntity entity, object domain)
        {
            if (domain == null)
            {
                return true;
            }

            var dataNeedToCompare = (Hatfield.EnviroData.DataProfile.WQ.Models.LabReportSample)entity;
            var dataToCompare = (Hatfield.EnviroData.Core.Action)domain;

            if (dataNeedToCompare == null || dataToCompare == null)
            {
                throw new InvalidCastException("Entity or domain is not supported by the site domain builder.");
            }

            return !WaterQualityEntityComparer.AreValueEqual(dataNeedToCompare, dataToCompare);
        }

    }
}
