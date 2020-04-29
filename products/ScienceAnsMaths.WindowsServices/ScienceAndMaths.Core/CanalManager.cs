﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Domain;
using ScienceAndMaths.Hydraulics.Canals;
using ScienceAndMaths.Shared.Canals;

namespace ScienceAndMaths.Core
{
    public class CanalManager : ICanalManager
    {
        public Canal Canal { get; private set; }

        public void SetCanal(Canal newCanal)
        {
            Canal = newCanal;
        }

        public CanalSimulationResult ExecuteCanalSimulation()
        {
            //  Coordinate and sync calls to make sure no race conditions and odd stuff can happen in sync
            return Canal.ExecuteCanalSimulation();
        }
    }
}
