using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Shared.Canals
{
    public class CanalGeometryData : ICanalSection, ICanalStretch
    {
        public double Roughness { get; set; }
        public double Slope { get; set; }
        public double Length { get; set; }
        public double Flow { get; set; }
        public string Id { get; set; }
    }
}
