using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Configuration.Canals;
using ScienceAndMaths.Hydraulics.Canals;
using ScienceAndMaths.Shared;

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
                data.CanelEdges.Add(new CanalEdge()
                {
                    Id =  node.NodeId
                });
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

                CanalEdge fromNode = data.CanelEdges.Single(ce => ce.Id == arrow.FromNodeId);
                CanalEdge toNode = data.CanelEdges.Single(ce => ce.Id == arrow.ToNodeId);

                data.CanalStretches.Add(new CanalStretch()
                {
                    Length = arrow.Length,
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
                configuration.Nodes.Add(new CanalNode()
                {
                    NodeId = edges.Id
                });
            }

            foreach (CanalStretch canalStretch in data.CanalStretches)
            {
                IBaseCanalSection canalSection;

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
