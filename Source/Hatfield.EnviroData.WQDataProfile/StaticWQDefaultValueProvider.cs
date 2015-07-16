using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Hatfield.EnviroData.WQDataProfile
{
    public class StaticWQDefaultValueProvider : IWQDefaultValueProvider
    {
        public string Name { get { return "Static Water Quality Default Value Provider"; } }

        // Action Default Values
        public string ActionTypeCVSampleCollection { get { return "Specimen collection"; } }
        public string ActionRelationshipTypeCVSampleCollection { get { return "Is related to"; } }
        public string ActionTypeCVChemistry { get { return "Specimen analysis"; } }
        public string ActionRelationshipTypeCVChemistry { get { return "Is child of"; } }

        // Person Default Values
        public string DefaultPersonFirstName { get { return "Unknown"; } }
        public string DefaultPersonMiddleName { get { return "Unknown"; } }
        public string DefaultPersonLastName { get { return "Unknown"; } }

        // Organization Default Values
        public string OrganizationTypeCVSampleCollection { get { return "Company"; } }
        public string OrganizationNameSampleCollection { get { return "Hatfield"; } }

        public string OrganizationTypeCVChemistry { get { return "Laboratory"; } }

        // Processing Level Default Values
        public string DefaultProcessingLevelCode { get { return "Unknown"; } }

        // Result Default Values
        public string ResultTypeCVSampleCollection { get { return "Measurement"; } }
        public string ResultSampledMediumCVSampleCollection { get { return "Liquid aqueous"; } }

        public string ResultTypeCVChemistry { get { return "Measurement"; } }
        public string ResultSampledMediumCVChemistry { get { return "Liquid aqueous"; } }

        // Sampling Feature Default Values
        public string DefaultSamplingFeatureTypeCVSampleCollection { get { return "Specimen"; } }

        public string DefaultSamplingFeatureTypeCVChemistry { get { return "Site"; } }

        public Guid DefaultSamplingFeatureUUID { get { return System.Guid.Empty; } }
        public string DefaultSamplingFeatureCode { get { return "Unknown"; } }

        // Measurement Result Default Values
        public string MeasurementResultCensorCodeCVChemistry { get { return "Not censored"; } }
        public string MeasurementResultQualityCodeCVChemistry { get { return "Unknown"; } }
        public string MeasurementResultAggregationStatisticCVChemistry { get { return "Unknown"; } }

        // Method Default Values
        public string DefaultMethodTypeCVSampleCollection { get { return "Specimen collection"; } }

        public string DefaultMethodTypeCVChemistry { get { return "Specimen analysis"; } }

        // Variable Default Values
        public string DefaultVariableTypeCVSampleCollection { get { return "Chemistry"; } }

        public string DefaultVariableTypeCVChemistry { get { return "Chemistry"; } }

        public string DefaultVariableCode { get { return "Unknown"; } }
        public string DefaultVariableNameCV { get { return "Unknown"; } }
        public string DefaultVariableSpeciationCV { get { return "Unknown"; } }
        public double DefaultVariableNoDataValue { get { return double.MinValue; } }

        // Unit Default Values
        public string DefaultUnitsTypeCVSampleCollection { get { return "Dimensionless"; } }

        public string DefaultUnitsTypeCVChemistry { get { return "Action"; } }

        // Dataset Default Values
        public string DefaultDatasetTypeCV { get { return "Other"; } }

        //Spatial Reference Default Values
        public string DefaultSRSCode { get { return "Unknown"; } }
        public string DefaultSRSName { get { return "Unknown"; } }
        public string DefaultSRSDescription { get { return "Unknown"; } }
        public string DefaultSRSLink { get { return "Unknown"; } }

        public WayToHandleNewData WayToHandleNewData
        {
            get 
            {
                return WayToHandleNewData.CreateInstanceForNewData;
            }        
        }


        public bool SaveDefaultValueConfiguration(WQDefaultValueModel data)
        {
            //For static values, do nothing
            return true;
        }


        public void Init()
        {
            //Do nothing to initialize
        }
    }
}
