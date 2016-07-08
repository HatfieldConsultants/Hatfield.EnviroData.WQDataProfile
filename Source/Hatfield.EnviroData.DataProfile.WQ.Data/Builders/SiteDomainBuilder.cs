using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Hatfield.EnviroData.DataProfile.WQ.Models;

namespace Hatfield.EnviroData.DataProfile.WQ.Builders
{
    public class SiteDomainBuilder : IDomainBuilder
    {
        public DomainBuildResult Build(Hatfield.EnviroData.DataProfile.WQ.Models.WQProfileEntity entity, Hatfield.EnviroData.Core.ODM2Entities dbContext)
        {
            var data = (Hatfield.EnviroData.DataProfile.WQ.Models.Site)entity;
            Hatfield.EnviroData.Core.Site domain = null;
            System.Data.Entity.EntityState state = EntityState.Unchanged;

            if(data == null)
            {
                throw new NullReferenceException("System fail to build domain on null entity.");
            }

            if (data.Id > 0)
            {
                domain = dbContext.Sites.Where(x => x.SamplingFeatureID == data.Id).FirstOrDefault();
            }

            //data has not change, return the data in the database
            if (!IsDataDirty(data, domain))
            {
                return new DomainBuildResult(true, domain, EntityState.Unchanged);
            }

            //data is dirty
            if (domain == null)
            {
                domain = new Core.Site();
                state = EntityState.Added;
            }
            else
            {
                state = EntityState.Modified;
            }

            domain.Latitude = data.Latitude.HasValue ? data.Latitude.Value : domain.Latitude;
            domain.Longitude = data.Longitude.HasValue ? data.Longitude.Value : domain.Longitude;
            domain.SamplingFeature.SamplingFeatureName = data.Name;
            var result = new DomainBuildResult(true, domain, state);
            return result;
        }


        public bool IsDataDirty(WQProfileEntity entity, object domain)
        {
            if(domain == null)
            {
                return true;
            }

            var dataNeedToCompare = (Hatfield.EnviroData.DataProfile.WQ.Models.Site)entity;
            var dataToCompare = (Hatfield.EnviroData.Core.Site)domain;

            if(dataNeedToCompare == null || dataToCompare == null)
            {
                throw new InvalidCastException("Entity or domain is not supported by the site domain builder.");
            }

            return !WaterQualityEntityComparer.AreValueEqual(dataNeedToCompare, dataToCompare);
        }
    }
}
