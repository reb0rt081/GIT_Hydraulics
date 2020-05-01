using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;
using ScienceAndMaths.Client.Shared;

namespace ScienceAndMaths.Client.Core.Helpers
{
    public static class RegionManagerExtensionMethods
    {
        public static readonly Dictionary<string, string> RegistrationMapping = new Dictionary<string, string>();

        public static void NavigateToViewAndRibbon(this IRegionManager regionManager, string regionName, string source)
        {
            regionManager.RequestNavigate(regionName, source);


            if (RegistrationMapping.TryGetValue(source, out string ribbonName))
            {
                regionManager.RequestNavigate(Constants.RibbonRegion, ribbonName);
            }
        }
    }
}
