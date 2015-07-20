using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hatfield.EnviroData.WQDataProfile
{
    public interface IWQDefaultValueProvider
    {
        string Name { get; }

        // Action Default Values
        string ActionTypeCVSampleCollection { get; }
        string ActionRelationshipTypeCVSampleCollection { get; }

        string ActionTypeCVChemistry { get; }
        string ActionRelationshipTypeCVChemistry { get; }

        // Person Default Values
        string DefaultPersonFirstName { get; }
        string DefaultPersonMiddleName { get; }
        string DefaultPersonLastName { get; }

        //Organization Default Values
        string DefaultOrganizationTypeCV { get; }
        string DefaultOrganizationName { get; }
        string DefaultOrganizationCode { get; }

        // Organization Default Values
        string OrganizationTypeCVSampleCollection { get; }
        string OrganizationNameSampleCollection { get; }

        string OrganizationTypeCVChemistry { get; }

        // Processing Level Default Values
        string DefaultProcessingLevels { get; }
        string DefaultProcessingLevelCode { get; }

        // Result Default Values
        string ResultTypeCVSampleCollection { get; }
        string ResultSampledMediumCVSampleCollection { get; }

        string ResultTypeCVChemistry { get; }
        string ResultSampledMediumCVChemistry { get; }

        // Sampling Feature Default Values
        string DefaultSamplingFeatureTypeCVSampleCollection { get; }

        string DefaultSamplingFeatureTypeCVChemistry { get; }

        Guid DefaultSamplingFeatureUUID { get; }
        string DefaultSamplingFeatureCode { get; }

        // Measurement Result Default Values
        string MeasurementResultCensorCodeCVChemistry { get; }
        string MeasurementResultQualityCodeCVChemistry { get; }
        string MeasurementResultAggregationStatisticCVChemistry { get; }

        // Method Default Values
        string DefaultMethodTypeCVSampleCollection { get; }

        string DefaultMethodTypeCVChemistry { get; }

        // Variable Default Values
        string DefaultVariableTypeCVSampleCollection { get; }

        string DefaultVariableTypeCVChemistry { get; }

        string DefaultVariableCode { get; }
        string DefaultVariableNameCV { get; }
        string DefaultVariableSpeciationCV { get; }
        double DefaultVariableNoDataValue { get; }

        // Unit Default Values
        string DefaultUnitsTypeCVSampleCollection { get; }

        string DefaultUnitsTypeCVChemistry { get; }

        // Dataset Default Values
        string DefaultDatasetTypeCV { get; }

        //Spatial Reference Default Values
        string DefaultSRSCode { get;  }
        string DefaultSRSName { get;  }
        string DefaultSRSDescription { get; }
        string DefaultSRSLink { get;  }

        WayToHandleNewData WayToHandleNewData { get; }

        void Init();
        bool SaveDefaultValueConfiguration(WQDefaultValueModel data);
    }
}
