using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Shared
{
    public interface ICanalSection
    {
        /// <summary>
        /// Roughness coefficient applying Manning's rule
        /// </summary>
        double Roughness { get; set; }

        /// <summary>
        /// Slope of the canal section in 1/1 (m/m)
        /// </summary>
        double Slope { get; set; }
    }
}
