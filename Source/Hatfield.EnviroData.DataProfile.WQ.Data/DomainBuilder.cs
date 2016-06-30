using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Hatfield.EnviroData.DataProfile.WQ
{
    /// <summary>
    /// 
    /// </summary>
    public static class DomainBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static DomainBuildResult<Hatfield.EnviroData.Core.Site> Build(Hatfield.EnviroData.DataProfile.WQ.Models.Site data, DbContext dbContext)
        {
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
            var result = new DomainBuildResult<Hatfield.EnviroData.Core.Site>(domain, state);
            return result;
        }
    }
}
