using System;
using System.Runtime.Serialization;
using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Configuration.Canals
{
    [DataContract(Namespace = XmlConsts.XmlNamespace)]
    [Serializable]
    public class RectangularSectionConfiguration : ICanalSection
    {
        [DataMember]
        public double Width { get; set; }

        [DataMember]
        public double Roughness { get; set; }

        [DataMember]
        public double Slope { get; set; }
    }
}
