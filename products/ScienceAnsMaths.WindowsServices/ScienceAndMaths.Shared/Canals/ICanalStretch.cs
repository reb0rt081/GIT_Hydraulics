using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Shared.Canals
{
    public interface ICanalStretch
    {
        /// <summary>
        /// Gets or sets the length of the canal
        /// </summary>
        double Length { get; set; }

        /// <summary>
        /// Gets or sets the flow of the canal
        /// </summary>
        double Flow { get; set; }

    }
}
