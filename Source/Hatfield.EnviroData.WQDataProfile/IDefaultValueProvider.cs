using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hatfield.EnviroData.WQDataProfile
{
    public interface IDefaultValueProvider
    {
        string Name { get; }

        //Person Default Values
        string DefaultPersonFirstName { get; }
        string DefaultPersonMiddleName { get; }
        string DefaultPersonLastName { get; }

        //Organization Default Values
        string DefaultOrganizationTypeCV { get; }
        string DefaultOrganizationName { get; }
        string DefaultOrganizationCode { get; }

        //Processing Level Default Values
        string DefaultProcessingLevels { get; }

        //Sampling Features Default Values
        string DefaultSamplingFeatureUUID { get; }
        string DefaultSamplingFeatureTypeCV { get; }
        string DefaultSamplingFeatureCode { get; }

        //Method Default Values
        string DefaultMethodTypeCV { get; }
        string DefaultMethodCode { get; }
        string DefaultMethodName { get; }
        string DefaultMethodDescription { get; }
        string DefaultMethodLink { get; }
        string DefaultMethodOrganizationID { get; }

        //Variable Default Values
        string DefaultVariableTypeCV { get; }
        string DefaultVariableCode { get; }
        string DefaultVariableNameCV { get; }
        string DefaultVariableDefinition { get; }
        string DefaultVariableSpeciationCV { get; }
        string DefaultVariableNoDataValue { get; }

        //Units Default Values
        string DefaultUnitsTypeCV { get; }
        string DefaultUnitsAbbreviation { get; }
        string DefaultUnitsName { get; }
        string DefaultUnitsLink { get; }

        //CV Default Values
        string DefaultCVUnitsType { get; }
        string DefaultCVTerm { get; }
        string DefaultCVName { get; }
    }
}
