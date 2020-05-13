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

            canal.CanelEdges.Add(new CanalEdge()
            {
                Id = "SourceCanal",
                WaterLevel = 2.9
            });

            canal.CanelEdges.Add(new CanalEdge()
            {
                Id = "EndCanal"
            });

            CanalStretch canalStretch = new CanalStretch(602, 20.32, new RectangularSection(5, 0.028, 0));
            canalStretch.FromNode = canal.CanelEdges.First();
            canalStretch.ToNode = canal.CanelEdges.Last();

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
