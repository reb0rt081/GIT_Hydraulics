using System;
using System.Runtime.Serialization;
using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Data.Canals
{
    [DataContract(Namespace = XmlConsts.XmlNamespace)]
    [Serializable]
    public class RectangularSection : ICanalSection
    {
        [DataMember]
        public double Width { get; set; }

        [DataMember]
        public double Roughness { get; set; }

        [DataMember]
        public double Slope { get; set; }
    }
}
