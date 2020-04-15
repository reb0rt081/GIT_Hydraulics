using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Data.Canals;
using ScienceAndMaths.Hydraulics.Canals;

namespace ScienceAndMaths.Domain
{
    public interface ICanalConfigurationLoader
    {
        Canal LoadCanalConfiguration(CanalConfiguration canalConfiguration);
    }
}
