using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using ScienceAndMaths.Configuration.Canals;
using ScienceAndMaths.Configuration.Converter;
using ScienceAndMaths.Domain;
using ScienceAndMaths.Hydraulics.Canals;
using ScienceAndMaths.Shared;

using Unity;

namespace ScienceAndMaths.Configuration.Loader
{
    public class CanalConfigurationLoader : ICanalConfigurationLoader
    {
        [Dependency]
        public ICanalManager CanalManager { get; set; }

        public void LoadCanalConfiguration()
        {
            string relativePath = ConfigurationManager.AppSettings[ServerConfigConsts.RelativeModelsLocation];
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string subFolderPath = Path.Combine(path, relativePath + "\\CanalConfiguration.xml");
            var canal = LoadCanalConfiguration(subFolderPath);

            CanalManager.SetCanal(canal);
        }

        public Canal LoadCanalConfiguration(string configurationLocation)
        {
            string fileContent;

            if (string.IsNullOrEmpty(configurationLocation))
            {
                throw new ArgumentNullException(nameof(configurationLocation), "No file location was passed");
            }

            using (StreamReader streamReader = File.OpenText(configurationLocation))
            {
                fileContent = streamReader.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(fileContent))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(CanalConfiguration));
                CanalConfiguration deserializedConfiguration;

                using (StringReader sr = new StringReader(fileContent))
                {
                    deserializedConfiguration = (CanalConfiguration)xmlSerializer.Deserialize(sr);
                }

                if (deserializedConfiguration != null)
                {
                    CanalConfigurationConverter converter = new CanalConfigurationConverter();
                    return converter.Convert(deserializedConfiguration);
                }
            }

            throw new ArgumentException($"It was not possible to load canal configuration from <{configurationLocation}>");
        }

        public void SaveCanalConfiguration(Canal canal)
        {
            string relativePath = ConfigurationManager.AppSettings[ServerConfigConsts.RelativeModelsLocation];
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string subFolderPath = Path.Combine(path, relativePath);
            
            SaveCanalConfiguration(canal, subFolderPath);
        }

        public void SaveCanalConfiguration(Canal canal, string saveLocation)
        {
            if (!Directory.Exists(saveLocation))
            {
                Directory.CreateDirectory(saveLocation);
            }

            string xml;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CanalConfiguration));

            CanalConfigurationConverter converter = new CanalConfigurationConverter();

            CanalConfiguration canalConfiguration = converter.ConvertBack(canal);

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, canalConfiguration);
                    xml = sww.ToString(); // Your XML
                }

                File.WriteAllText(Path.Combine(saveLocation, "CanalConfiguration.xml"), xml);
            }
        }

        private Canal GetAndValidateCanalConfiguration(CanalConfiguration configuration)
        {
            if (string.IsNullOrEmpty(configuration.ConfigId))
            {
                throw new ArgumentNullException(nameof(configuration.ConfigId), "The config Id must have a name");
            }

            if (configuration.Arrows == null || 
                configuration.Arrows.Count == 0 || 
                configuration.Nodes == null ||
                configuration.Nodes.Count == 0)
            {
                throw new ArgumentException($"{typeof(CanalConfiguration).Name} must have arrows and nodes!");
            }

            if (configuration.Arrows.Any(ar => string.IsNullOrEmpty(ar.FromNodeId) || 
                                               string.IsNullOrEmpty(ar.ToNodeId) || 
                                               !configuration.Nodes.Select(nd => nd.NodeId).Contains(ar.FromNodeId) || 
                                               !configuration.Nodes.Select(nd => nd.NodeId).Contains(ar.ToNodeId)))
            {
                throw new ArgumentException($"{typeof(CanalConfiguration).Name} must have nodes configured for all arrows!");
            }

            return new Canal();
        }
    }
}
