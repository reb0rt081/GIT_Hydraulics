using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ScienceAndMaths.Configuration.Canals;
using ScienceAndMaths.Domain;
using ScienceAndMaths.Hydraulics.Canals;

namespace ScienceAndMaths.Configuration.Loader
{
    public class CanalConfigurationLoader : ICanalConfigurationLoader
    {
        private ICanalManager canalManager;

        public void LoadCanalConfiguration()
        {
            var canal = LoadCanalConfiguration("test");

            canalManager.SetCanal(canal);
        }

        public Canal LoadCanalConfiguration(string configurationLocation)
        {
            string fileContent = null;

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
                CanalConfiguration deserializedConfiguration = null;

                using (StringReader sr = new StringReader(fileContent))
                {
                    deserializedConfiguration = (CanalConfiguration)xmlSerializer.Deserialize(sr);
                }

                if (deserializedConfiguration != null)
                {
                    return new Canal();
                }
            }

            throw new ArgumentException($"It was not possible to load canal configuration from <{configurationLocation}>");
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
