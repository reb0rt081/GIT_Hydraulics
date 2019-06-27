using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Hydraulics
{
    public abstract class CanalSection
    {
        public double HydraulicRadius
        {
            get { return HydraulicArea() / HydraulicPerimeter(); }
        }

        public abstract double HydraulicArea();

        public abstract double HydraulicPerimeter();
        
    }
}
