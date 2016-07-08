using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.DataProfile.WQ
{
    public class WaterQualityEntityComparer
    {
        public static bool AreValueEqual(Hatfield.EnviroData.DataProfile.WQ.Models.Site model, Hatfield.EnviroData.Core.Site domain)
        {
            if(model == null || domain == null)
            {
                return false;
            }

            var dataAreTheSame = (domain.Latitude == model.Latitude) &&
                                (domain.Longitude == model.Longitude) &&
                                (domain.SamplingFeature != null) &&
                                (domain.SamplingFeature.SamplingFeatureName == model.Name);

            return dataAreTheSame;
        }

        public static bool AreValueEqual(Hatfield.EnviroData.DataProfile.WQ.Models.Unit model, Hatfield.EnviroData.Core.Unit domain)
        {
            if (model == null || domain == null)
            {
                return false;
            }

            return model.Name == domain.UnitsName;
        }

        public static bool AreValueEqual(Hatfield.EnviroData.DataProfile.WQ.Models.Person model, Hatfield.EnviroData.Core.Person domain)
        {
            if (model == null || domain == null)
            {
                return false;
            }

            return string.Equals(model.FirstName, domain.PersonFirstName, StringComparison.InvariantCulture) &&
                    string.Equals(model.MiddleName, domain.PersonMiddleName, StringComparison.InvariantCulture) &&
                    string.Equals(model.LastName, domain.PersonLastName, StringComparison.InvariantCulture);
        }

        public static bool AreValueEqual(Hatfield.EnviroData.DataProfile.WQ.Models.Lab model, Hatfield.EnviroData.Core.Organization domain)
        {
            if (model == null || domain == null)
            {
                return false;
            }

            return string.Equals(model.Name, domain.OrganizationName, StringComparison.InvariantCulture);
        }

        public static bool AreValueEqual(Hatfield.EnviroData.DataProfile.WQ.Models.Analyte model, Hatfield.EnviroData.Core.Variable domain)
        {
            if (model == null || domain == null)
            {
                return false;
            }

            return string.Equals(model.Name, domain.VariableCode, StringComparison.InvariantCulture);
        }
    }
}
