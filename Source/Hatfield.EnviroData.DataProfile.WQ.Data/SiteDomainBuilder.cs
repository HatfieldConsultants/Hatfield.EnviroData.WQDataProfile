using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Hatfield.EnviroData.DataProfile.WQ.Models;

namespace Hatfield.EnviroData.DataProfile.WQ
{
    public class SiteDomainBuilder : IDomainBuilder
    {
        public DomainBuildResult Build(Models.WQProfileEntity entity, System.Data.Entity.DbContext dbContext)
        {
            var data = (Hatfield.EnviroData.DataProfile.WQ.Models.Site)entity;
            Hatfield.EnviroData.Core.Site domain = null;
            System.Data.Entity.EntityState state = EntityState.Unchanged;

            if (data.Id > 0)
            {
                domain = dbContext.Set<Core.Site>().Where(x => x.SamplingFeatureID == data.Id).FirstOrDefault();
            }

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
    }
}
