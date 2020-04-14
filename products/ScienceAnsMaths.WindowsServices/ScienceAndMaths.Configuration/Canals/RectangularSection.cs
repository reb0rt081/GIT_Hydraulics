using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Configuration.Canals
{
    [DataContract(Namespace = XmlConsts.XmlNamespace)]
    [Serializable]
    public class RectangularSection : ICanalSection
    {
        [DataMember]
        public double Roughness { get; set; }

        [DataMember]
        public double Slope { get; set; }

        [DataMember]
        public double Width { get; set; }
    }
}
