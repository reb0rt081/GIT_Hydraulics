using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ScienceAndMaths.Hydraulics.Canals;

namespace ScienceAndMaths.Core.Test
{
    [TestClass]
    public class CanalManagerTest
    {
        [TestMethod]
        public void BasicCanalTest()
        {
            CanalManager canalManager = new CanalManager();

            Canal canal = new Canal();

            canal.CanelEdges.Add(new CanalEdge()
            {
                Id = "SourceCanal",
                WaterLevel = 2.9
            });

            canal.CanelEdges.Add(new CanalEdge()
            {
                Id = "EndCanal"
            });

            CanalStretch canalStretch = new CanalStretch("stretch1", 602, 20.32, new RectangularSection(5, 0.028, 0));
            canalStretch.FromNode = canal.CanelEdges.First();
            canalStretch.ToNode = canal.CanelEdges.Last();

            canal.CanalStretches.Add(canalStretch);

            canalManager.SetCanal(canal);

            var result = canalManager.ExecuteCanalSimulation();

            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void BasicCanalTest2()
        {
            CanalManager canalManager = new CanalManager();

            Canal canal = new Canal();

            canal.CanelEdges.Add(new CanalEdge()
            {
                Id = "SourceCanal",
                WaterLevel = 2.9
            });

            canal.CanelEdges.Add(new CanalEdge()
            {
                Id = "EndCanal"
            });

            CanalStretch canalStretch = new CanalStretch("stretch1", 602, 20.32, new RectangularSection(5, 0.028, 0));
            canalStretch.FromNode = canal.CanelEdges.First();
            canalStretch.ToNode = canal.CanelEdges.Last();

            canal.CanalStretches.Add(canalStretch);

            canalManager.SetCanal(canal);

            var result = canalManager.ExecuteCanalSimulation();

            Assert.IsTrue(result != null);
        }
    }
}
