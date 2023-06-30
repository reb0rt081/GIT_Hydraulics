using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Configuration.Canals;
using ScienceAndMaths.Hydraulics.Canals;
using ScienceAndMaths.Shared;
using RectangularSectionConfiguration = ScienceAndMaths.Configuration.Canals.RectangularSectionConfiguration;

namespace ScienceAndMaths.Configuration.Converter
{
    public abstract class BaseConfigurationConverter<T1, T2>
    {
        public abstract T2 Convert(T1 configuration);

        public abstract T1 ConvertBack(T2 data);
    }
}
