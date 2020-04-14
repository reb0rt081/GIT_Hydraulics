using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Domain;
using ScienceAndMaths.Hydraulics.Canals;
using ScienceAndMaths.Shared.Canals;

namespace ScienceAndMaths.Application
{
    public class CanalFlowService : ICanalFlowService
    {
        public CanalSimulationResult ExecuteCanalSimulation(Canal canal)
        {
            return new CanalSimulationResult();
        }
    }
}
