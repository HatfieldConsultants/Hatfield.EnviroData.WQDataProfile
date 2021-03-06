﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hatfield.EnviroData.WQDataProfile
{
    public class WQDefaultValueModel
    {
        // Action Default Values
        public string ActionTypeCVSampleCollection { get; set; }
        public string ActionRelationshipTypeCVSampleCollection { get; set; }

        public string ActionTypeCVChemistry { get; set; }
        public string ActionRelationshipTypeCVChemistry { get; set; }
        public string ActionRelationshipTypeSubVersion { get; set; }

        // Person Default Values
        public string DefaultPersonFirstName { get; set; }
        public string DefaultPersonMiddleName { get; set; }
        public string DefaultPersonLastName { get; set; }

        //Organization Default Values
        public string DefaultOrganizationTypeCV { get; set; }
        public string DefaultOrganizationName { get; set; }
        public string DefaultOrganizationCode { get; set; }

        // Organization Default Values
        public string OrganizationTypeCVSampleCollection { get; set; }
        public string OrganizationNameSampleCollection { get; set; }

        public string OrganizationTypeCVChemistry { get; set; }

        // Processing Level Default Values
        public string DefaultProcessingLevelCode { get; set; }

        // Result Default Values
        public string ResultTypeCVSampleCollection { get; set; }
        public string ResultSampledMediumCVSampleCollection { get; set; }

        public string ResultTypeCVChemistry { get; set; }
        public string ResultSampledMediumCVChemistry { get; set; }

        // Sampling Feature Default Values
        public string DefaultSamplingFeatureTypeCVSampleCollection { get; set; }
        public string DefaultSamplingFeatureNameSampleCollection { get; set; }
        public string DefaultSamplingFeatureCodeSampleCollection { get; set; }
        public Guid DefaultSamplingFeatureUUIDSampleCollection { get; set; }

        public string DefaultSamplingFeatureTypeCVChemistry { get; set; }
        public string DefaultSamplingFeatureNameChemistry { get; set; }
        public string DefaultSamplingFeatureCodeChemistry { get; set; }
        public Guid DefaultSamplingFeatureUUIDChemistry { get; set; }

        // Measurement Result Default Values
        public string MeasurementResultCensorCodeCVChemistry { get; set; }
        public string MeasurementResultQualityCodeCVChemistry { get; set; }
        public string MeasurementResultAggregationStatisticCVChemistry { get; set; }

        // Method Default Values
        public string DefaultMethodTypeCVSampleCollection { get; set; }

        public string DefaultMethodTypeCVChemistry { get; set; }

        // Variable Default Values
        public string DefaultVariableTypeCVSampleCollection { get; set; }

        public string DefaultVariableTypeCVChemistry { get; set; }

        public string DefaultVariableCode { get; set; }
        public string DefaultVariableNameCV { get; set; }
        public string DefaultVariableSpeciationCV { get; set; }
        public double DefaultVariableNoDataValue { get; set; }

        // Unit Default Values
        public string DefaultUnitsTypeCVSampleCollection { get; set; }

        public string DefaultUnitsTypeCVChemistry { get; set; }

        // Dataset Default Values
        public string DefaultDatasetTypeCV { get; set; }

        // Spatial Reference Default Values
        public string DefaultSRSCode { get; set; }
        public string DefaultSRSName { get; set; }
        public string DefaultSRSDescription { get; set; }
        public string DefaultSRSLink { get; set; }

        public WayToHandleNewData WayToHandleNewData { get; set; }
    }
}
