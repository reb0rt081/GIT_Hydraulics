using System;
using System.Runtime.Serialization;
using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Configuration.Canals
{
    [DataContract(Namespace = XmlConsts.XmlNamespace)]
    [Serializable]
    public class TrapezoidalSectionConfiguration : ICanalSection
    {
        [DataMember]
        public double Width { get; set; }

        [DataMember]
        public double Roughness { get; set; }

        [DataMember]
        public double Slope { get; set; }

        [DataMember]
        public double Incline { get; set; }
    }
}
