using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScienceAndMaths.Configuration.Canals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ScienceAndMaths.Configuration.Converter;

namespace ScienceAndMaths.Configuration.Test
{
    [TestClass]
    public class CanalConfigurationTest
    {
        [TestMethod]
        public void RectangularSerializingTest()
        {
            //  Arrange
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CanalConfiguration));
            CanalConfiguration canalConfiguration = new CanalConfiguration();            
            canalConfiguration.ConfigId = "TestConfig";
            
            CanalNode initNode = new CanalNode()
            {
                NodeId = "InitNode"
            };
            CanalNode endNode = new CanalNode()
            {
                NodeId = "EndNode"
            };
            CanalArrow arrow = new CanalArrow()
            {
                ArrowId = "TestArrow",
                Length = 500.0,
                FromNodeId = initNode.NodeId,
                ToNodeId = endNode.NodeId
            };
            arrow.CanalSection = new RectangularSectionConfiguration()
            {
                Roughness = 0.028,
                Slope = 0.001,
                Width = 5.0
            };
            canalConfiguration.Arrows = new List<CanalArrow>() { arrow };
            canalConfiguration.Nodes = new List<CanalNode>() { initNode, endNode };
            var xml = "";

            //  Act
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, canalConfiguration);
                    xml = sww.ToString(); // Your XML
                }
            }

            //  Assert
            Assert.IsTrue(xml != null);

            CanalConfiguration deserializedConfiguration = null;

            using (StringReader sr = new StringReader(xml))
            {
                deserializedConfiguration = (CanalConfiguration)xmlSerializer.Deserialize(sr);
            }

            Assert.IsTrue(deserializedConfiguration != null);
            Assert.AreEqual(2, deserializedConfiguration.Nodes.Count);
            Assert.AreEqual(1, deserializedConfiguration.Arrows.Count);

            CanalConfigurationConverter converter = new CanalConfigurationConverter();

            var data = converter.Convert(deserializedConfiguration);

            Assert.IsTrue(data != null);
            Assert.AreEqual(2, data.CanelEdges.Count);
            Assert.AreEqual(1, data.CanalStretches.Count);

            canalConfiguration = converter.ConvertBack(data);

            Assert.IsTrue(canalConfiguration != null);
            Assert.AreEqual(2, canalConfiguration.Nodes.Count);
            Assert.AreEqual(1, canalConfiguration.Arrows.Count);
        }

        [TestMethod]
        public void TrapezoidSerializingTest()
        {
            //  Arrange
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CanalConfiguration));
            CanalConfiguration canalConfiguration = new CanalConfiguration();
            canalConfiguration.ConfigId = "TestConfig";

            CanalNode initNode = new CanalNode()
            {
                NodeId = "InitNode"
            };
            CanalNode endNode = new CanalNode()
            {
                NodeId = "EndNode"
            };
            CanalArrow arrow = new CanalArrow()
            {
                ArrowId = "TestArrow",
                Length = 500.0,
                FromNodeId = initNode.NodeId,
                ToNodeId = endNode.NodeId
            };
            arrow.CanalSection = new TrapezoidalSectionConfiguration()
            {
                Roughness = 0.028,
                Slope = 0.001,
                Width = 5.0,
                Incline = 0.5
            };
            canalConfiguration.Arrows = new List<CanalArrow>() { arrow };
            canalConfiguration.Nodes = new List<CanalNode>() { initNode, endNode };
            var xml = "";

            //  Act
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, canalConfiguration);
                    xml = sww.ToString(); // Your XML
                }
            }

            //  Assert
            Assert.IsTrue(xml != null);
            CanalConfiguration deserializedConfiguration = null;

            using (StringReader sr = new StringReader(xml))
            {
                deserializedConfiguration =  (CanalConfiguration)xmlSerializer.Deserialize(sr);
            }

            Assert.IsTrue(deserializedConfiguration != null);
            Assert.AreEqual(2, deserializedConfiguration.Nodes.Count);
            Assert.AreEqual(1, deserializedConfiguration.Arrows.Count);

            CanalConfigurationConverter converter = new CanalConfigurationConverter();

            var data = converter.Convert(deserializedConfiguration);

            Assert.IsTrue(data != null);
            Assert.AreEqual(2, data.CanelEdges.Count);
            Assert.AreEqual(1, data.CanalStretches.Count);

            canalConfiguration = converter.ConvertBack(data);

            Assert.IsTrue(canalConfiguration != null);
            Assert.AreEqual(2, canalConfiguration.Nodes.Count);
            Assert.AreEqual(1, canalConfiguration.Arrows.Count);
        }
    }
}
