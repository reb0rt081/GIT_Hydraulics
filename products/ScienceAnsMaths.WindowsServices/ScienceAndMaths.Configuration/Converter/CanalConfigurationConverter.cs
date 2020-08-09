using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Configuration.Canals;
using ScienceAndMaths.Hydraulics.Canals;
using ScienceAndMaths.Shared.Canals;

namespace ScienceAndMaths.Configuration.Converter
{
    public class CanalConfigurationConverter : BaseConfigurationConverter<CanalConfiguration, Canal>
    {
        public override Canal Convert(CanalConfiguration configuration)
        {
            Canal data = new Canal();
            data.Id = configuration.ConfigId;

            foreach (CanalNode node in configuration.Nodes)
            {
                if (node is SluiceGateNode sluiceGateNode)
                {
                    data.CanelEdges.Add(new SluiceCanalEdge()
                    {
                        Id = sluiceGateNode.NodeId,
                        WaterLevel = sluiceGateNode.WaterLevel,
                        ContractionCoefficient = sluiceGateNode.ContractionCoefficient,
                        RetainedWaterLevel = sluiceGateNode.RetainedWaterLevel,
                        GateOpening = sluiceGateNode.GateOpening
                    });
                }
                else
                {
                    data.CanelEdges.Add(new CanalEdge()
                    {
                        Id = node.NodeId,
                        WaterLevel = node.WaterLevel
                    });
                }
                
            }

            foreach (CanalArrow arrow in configuration.Arrows)
            {
                CanalSection canalSection;

                if (arrow.CanalSection is RectangularSectionConfiguration rectangularSection)
                {
                    canalSection = new RectangularSection(rectangularSection.Width, rectangularSection.Roughness, rectangularSection.Slope);
                }
                else if (arrow.CanalSection is TrapezoidalSectionConfiguration trapezoidalSection)
                {
                    canalSection = new RectangularSection(trapezoidalSection.Width, trapezoidalSection.Roughness, trapezoidalSection.Slope);
                }
                else
                {
                    throw new ArgumentException($"Canal section for arrow {arrow.ArrowId} is not valid.");
                }

                ICanalEdge fromNode = data.CanelEdges.Single(ce => ce.Id == arrow.FromNodeId);
                ICanalEdge toNode = data.CanelEdges.Single(ce => ce.Id == arrow.ToNodeId);

                data.CanalStretches.Add(new CanalStretch()
                {
                    Length = arrow.Length,
                    Flow = arrow.Flow,
                    CanalSection = canalSection,
                    FromNode = fromNode,
                    ToNode = toNode,
                    Id = arrow.ArrowId
                });
            }

            return data;
        }

        public override CanalConfiguration ConvertBack(Canal data)
        {
            CanalConfiguration configuration = new CanalConfiguration();

            configuration.ConfigId = data.Id;
            configuration.Arrows = new List<CanalArrow>();
            configuration.Nodes = new List<CanalNode>();

            foreach (CanalEdge edges in data.CanelEdges)
            {
                if (edges is SluiceCanalEdge sluiceCanalEdge)
                {
                    configuration.Nodes.Add(new SluiceGateNode()
                    {
                        NodeId = sluiceCanalEdge.Id,
                        WaterLevel = sluiceCanalEdge.WaterLevel,
                        ContractionCoefficient = sluiceCanalEdge.ContractionCoefficient,
                        RetainedWaterLevel = sluiceCanalEdge.RetainedWaterLevel,
                        GateOpening = sluiceCanalEdge.GateOpening
                    });
                }
                else
                {
                    configuration.Nodes.Add(new CanalNode()
                    {
                        NodeId = edges.Id,
                        WaterLevel = edges.WaterLevel,
                    });
                }
            }

            foreach (CanalStretch canalStretch in data.CanalStretches)
            {
                ICanalSection canalSection;

                if (canalStretch.CanalSection is RectangularSection rectangularSection)
                {
                    canalSection = new RectangularSectionConfiguration()
                    {
                        Width = rectangularSection.Width,
                        Roughness = rectangularSection.Roughness,
                        Slope = rectangularSection.Slope
                    };
                }
                else
                {
                    throw new ArgumentException($"Canal section for stretch {canalStretch.Id} is not valid.");
                }

                configuration.Arrows.Add(new CanalArrow()
                {
                    Length = canalStretch.Length,
                    Flow = canalStretch.Flow,
                    CanalSection = canalSection,
                    FromNodeId = canalStretch.FromNode.Id,
                    ToNodeId = canalStretch.ToNode.Id,
                    ArrowId = canalStretch.Id
                });
            }

            return configuration;
        }
    }
}
