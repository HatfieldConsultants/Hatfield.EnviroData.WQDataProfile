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

        public static bool AreValueEqual(Hatfield.EnviroData.DataProfile.WQ.Models.LabReportSample model, Hatfield.EnviroData.Core.Action domain)
        {
            var observationResult = domain.FeatureActions.FirstOrDefault()
                                    .Results.FirstOrDefault()
                                    .MeasurementResult;

            var observationResultValue = observationResult.MeasurementResultValues.FirstOrDefault();
            var unitOfDomain = observationResult.Unit;
            var analyteOfDomain = observationResult.Result.Variable;
            var siteOfDomain = observationResult.Result.FeatureAction.SamplingFeature.Site;

            //var personOfDomain = observationResult.Result.;
            //var labOfDomain = domain;
            
            

            return model.Value == observationResultValue.DataValue && //value are equal
                    model.DateTime == observationResultValue.ValueDateTime && //result time are equal
                    AreValueEqual(model.Site, siteOfDomain) && //site are equal
                    AreValueEqual(model.Unit, unitOfDomain) && //unit are equal
                    //AreValueEqual(model.ImportBy, personOfDomain) && //importer person are equal
                    //AreValueEqual(model.Lab, labOfDomain) && //lab are equal
                    AreValueEqual(model.Analyte, analyteOfDomain);//analyte are equal
                    
        }
    }
}
