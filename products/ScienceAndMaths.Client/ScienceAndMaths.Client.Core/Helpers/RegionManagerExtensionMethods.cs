using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;
using ScienceAndMaths.Client.Shared;

namespace ScienceAndMaths.Client.Core.Helpers
{
    /// <summary>
    /// Example of how we could override the regionmanager to allow navigation to ribbons. Bear in mind the ideal solution is to do this overriding the navigation service
    /// </summary>
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
