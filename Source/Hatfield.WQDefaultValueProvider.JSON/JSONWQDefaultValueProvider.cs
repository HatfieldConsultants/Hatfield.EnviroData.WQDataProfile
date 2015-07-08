using Hatfield.EnviroData.WQDataProfile;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Hatfield.WQDefaultValueProvider.JSON
{
    public class JSONWQDefaultValueProvider : IWQDefaultValueProvider
    {
        private WQDefaultValueModel _data;
        private string _jsonFilePath;

        public JSONWQDefaultValueProvider(string jsonFilePath)
        {
            _jsonFilePath = jsonFilePath;
        }

        public string Name
        {
            get
            {
                return "JSON Default Value Provider";
            }
        }

        public string DefaultPersonFirstName
        {
            get
            {
                return _data.DefaultPersonFirstName;
            }
        }

        public string DefaultPersonMiddleName
        {
            get
            {
                return _data.DefaultPersonMiddleName;
            }
        }

        public string DefaultPersonLastName
        {
            get
            {
                return _data.DefaultPersonLastName;
            }
        }

        public string DefaultOrganizationTypeCV
        {
            get
            {
                return _data.DefaultOrganizationTypeCV;
            }
        }

        public string DefaultOrganizationName
        {
            get
            {
                return _data.DefaultOrganizationName;
            }
        }

        public string DefaultOrganizationCode
        {
            get
            {
                return _data.DefaultOrganizationCode;
            }
        }

        public string DefaultProcessingLevels
        {
            get
            {
                return _data.DefaultProcessingLevels;
            }
        }

        public string DefaultSamplingFeatureUUID
        {
            get
            {
                return _data.DefaultSamplingFeatureUUID;
            }
        }

        public string DefaultSamplingFeatureTypeCV
        {
            get
            {
                return _data.DefaultSamplingFeatureTypeCV;
            }
        }

        public string DefaultSamplingFeatureCode
        {
            get
            {
                return _data.DefaultSamplingFeatureCode;
            }
        }

        public string DefaultMethodTypeCV
        {
            get
            {
                return _data.DefaultMethodTypeCV;
            }
        }

        public string DefaultMethodCode
        {
            get
            {
                return _data.DefaultMethodCode;
            }
        }

        public string DefaultMethodName
        {
            get
            {
                return _data.DefaultMethodName;
            }
        }

        public string DefaultMethodDescription
        {
            get
            {
                return _data.DefaultMethodDescription;
            }
        }

        public string DefaultMethodLink
        {
            get { return _data.DefaultMethodLink; }
        }

        public string DefaultMethodOrganizationID
        {
            get { return _data.DefaultMethodOrganizationID; }
        }

        public string DefaultVariableTypeCV
        {
            get { return _data.DefaultVariableTypeCV; }
        }

        public string DefaultVariableCode
        {
            get { return _data.DefaultVariableCode; }
        }

        public string DefaultVariableNameCV
        {
            get { return _data.DefaultVariableNameCV; }
        }

        public string DefaultVariableDefinition
        {
            get { return _data.DefaultVariableDefinition; }
        }

        public string DefaultVariableSpeciationCV
        {
            get { return _data.DefaultVariableSpeciationCV; }
        }

        public double DefaultVariableNoDataValue
        {
            get { return _data.DefaultVariableNoDataValue; }
        }

        public string DefaultUnitsTypeCV
        {
            get { return _data.DefaultUnitsTypeCV; }
        }

        public string DefaultUnitsAbbreviation
        {
            get { return _data.DefaultUnitsAbbreviation; }
        }

        public string DefaultUnitsName
        {
            get { return _data.DefaultUnitsName; }
        }

        public string DefaultUnitsLink
        {
            get { return _data.DefaultUnitsLink; }
        }

        public string DefaultCVUnitsType
        {
            get { return _data.DefaultCVUnitsType; }
        }

        public string DefaultCVTerm
        {
            get { return _data.DefaultCVTerm; }
        }

        public string DefaultCVName
        {
            get { return _data.DefaultCVName; }
        }

        public WayToHandleNewData WayToHandleNewData
        {
            get { return _data.WayToHandleNewData; }
        }

        public bool SaveDefaultValueConfiguration(WQDefaultValueModel data)
        {
            _data = data;

            var fileMode = File.Exists(_jsonFilePath) ? FileMode.Open : FileMode.CreateNew;

            try
            {
                using (FileStream fs = File.Open(_jsonFilePath, fileMode))
                using (StreamWriter sw = new StreamWriter(fs))
                using (JsonWriter jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;

                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(jw, data);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public void Init()
        {
            if (File.Exists(_jsonFilePath))
            {
                try
                {
                    using (FileStream fs = File.Open(_jsonFilePath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(fs))
                        {
                            using (JsonReader jr = new JsonTextReader(reader))
                            {
                                JsonSerializer serializer = new JsonSerializer();
                                _data = serializer.Deserialize<WQDefaultValueModel>(jr);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidDataException("JSON provider initialize fail. The provided json file is not valid." + ex.StackTrace);
                }
            }
            else
            {
                throw new FileNotFoundException("JSON provider initialize fail. The provided path " + _jsonFilePath + " could not be found.");
            }
        }
    }
}