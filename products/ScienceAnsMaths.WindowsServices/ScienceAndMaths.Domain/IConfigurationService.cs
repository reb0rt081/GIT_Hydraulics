﻿using ScienceAndMaths.Hydraulics.Canals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Domain
{
    public interface IConfigurationService
    {
        Canal LoadCanalConfiguration(string file);
    }
}
