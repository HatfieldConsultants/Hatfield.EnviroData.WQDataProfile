using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Hatfield.EnviroData.WQDataProfile
{
    public class StaticWQDefaultValueProvider : IWQDefaultValueProvider
    {
        public string Name
        {
            get {
                return "Static Water Quality Default Value Provider";
            }
        }

        public string DefaultPersonFirstName
        {
            get {
                return "Unknown";
            }
        }

        public string DefaultPersonMiddleName
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultPersonLastName
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultOrganizationTypeCV
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultOrganizationName
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultOrganizationCode
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultProcessingLevels
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultSamplingFeatureUUID
        {
            get {
                return System.Guid.Empty.ToString();
            }
        }

        public string DefaultSamplingFeatureTypeCV
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultSamplingFeatureCode
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultMethodTypeCV
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultMethodCode
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultMethodName
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultMethodDescription
        {
            get
            {
                return null;
            }
        }

        public string DefaultMethodLink
        {
            get
            {
                return null;
            }
        }

        public string DefaultMethodOrganizationID
        {
            get
            {
                return null;
            }
        }

        public string DefaultVariableTypeCV
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultVariableCode
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultVariableNameCV
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultVariableDefinition
        {
            get
            {
                return null;
            }
        }

        public string DefaultVariableSpeciationCV
        {
            get
            {
                return "Unknown";
            }
        }

        public double DefaultVariableNoDataValue
        {
            get
            {
                return double.MinValue;
            }
        }

        public string DefaultUnitsTypeCV
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultUnitsAbbreviation
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultUnitsName
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultUnitsLink
        {
            get
            {
                return null;
            }
        }

        public string DefaultCVUnitsType
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultCVTerm
        {
            get
            {
                return "Unknown";
            }
        }

        public string DefaultCVName
        {
            get
            {
                return "Unknown";
            }
        }

        public WayToHandleNewData WayToHandleNewData
        {
            get 
            {
                return WayToHandleNewData.CreateInstanceForNewData;
            }        
        }
    }
}
