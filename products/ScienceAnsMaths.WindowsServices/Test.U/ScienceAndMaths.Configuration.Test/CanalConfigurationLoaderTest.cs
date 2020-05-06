using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ScienceAndMaths.Configuration.Canals;
using ScienceAndMaths.Configuration.Loader;
using ScienceAndMaths.Core;
using ScienceAndMaths.Hydraulics.Canals;
using ScienceAndMaths.Shared;
using ScienceAndMaths.Shared.Canals;

namespace ScienceAndMaths.Configuration.Test
{
    [TestClass]
    public class CanalConfigurationLoaderTest
    {
        [TestMethod]
        public void SaveAndLoadConfiguration()
        {
            //  Arrange
            CanalManager canalManager = new CanalManager();
            ConfigurationManager.AppSettings[ServerConfigConsts.RelativeModelsLocation] = "ScienceAndMaths";
            CanalConfigurationLoader loader = new CanalConfigurationLoader();
            loader.CanalManager = canalManager;

            Canal canal = new Canal
            {
                Id = "TestConfig"
            };
            CanalEdge initNode = new CanalEdge()
            {
                Id = "InitNode"
            };

            CanalEdge endNode = new CanalEdge()
            {
                Id = "EndNode"
            };

            canal.CanelEdges.Add(initNode);
            canal.CanelEdges.Add(endNode);

            CanalStretch canalStretch = new CanalStretch
            {
                Id = "TestStretch",
                CanalSection = new RectangularSection(5.0, 0.028, 0.001),
                Length = 500.0,
                FromNode = initNode,
                ToNode = endNode
            };

            canal.CanalStretches.Add(canalStretch);

            CanalConfiguration canalConfiguration = new CanalConfiguration();
            canalConfiguration.ConfigId = "TestConfig";

            loader.SaveCanalConfiguration(canal);

            loader.LoadCanalConfiguration();

            Assert.IsNotNull(canalManager.Canal);

            Assert.AreEqual(2, canalManager.Canal.CanelEdges.Count);
            Assert.AreEqual(1, canalManager.Canal.CanalStretches.Count);
        }
    }
}
