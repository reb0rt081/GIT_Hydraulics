using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Domain;
using ScienceAndMaths.Hydraulics.Canals;
using ScienceAndMaths.Shared.Canals;
using Unity;

namespace ScienceAndMaths.Application
{
    public class CanalFlowService : ICanalFlowService
    {
        [Dependency]
        public ICanalManager CanalManager { get; set; }

        public CanalSimulationResult ExecuteCanalSimulation()
        {
            return new CanalSimulationResult();
        }
    }
}
