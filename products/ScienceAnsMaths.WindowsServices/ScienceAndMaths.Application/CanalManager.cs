using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Domain;
using ScienceAndMaths.Hydraulics.Canals;

namespace ScienceAndMaths.Application
{
    public class CanalManager : ICanalManager
    {
        private Canal canal;
        public void SetCanal(Canal newCanal)
        {
            canal = newCanal;
        }
    }
}
