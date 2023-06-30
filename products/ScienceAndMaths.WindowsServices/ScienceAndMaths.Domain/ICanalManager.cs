using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Hydraulics.Canals;
using ScienceAndMaths.Shared;
using ScienceAndMaths.Shared.Canals;

namespace ScienceAndMaths.Domain
{
    public interface ICanalManager
    {
        void SetCanal(Canal newCanal);

        CanalData ExecuteCanalSimulation();
    }
}
