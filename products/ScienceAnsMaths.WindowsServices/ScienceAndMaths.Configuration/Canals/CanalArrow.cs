using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Configuration.Canals
{
    [DataContract(Namespace = XmlConsts.XmlNamespace)]
    [Serializable]
    public class CanalArrow
    {
        [DataMember]
        public string ArrowId { get; set; }

        [DataMember]
        public double Length { get; set; }

        [DataMember]
        public string FromNodeId { get; set; }

        [DataMember]
        public string ToNodeId { get; set; }

        [DataMember]
        public RectangularSection RectangularSectionn
        {
            get
            {
                return CanalSection as RectangularSection;
            }
            set
            {
                CanalSection = value;
            }
        }

        [DataMember]
        public TrapezoidalSection TrapezoidalSection
        {
            get
            {
                return CanalSection as TrapezoidalSection;
            }
            set
            {
                CanalSection = value;
            }
        }

        [XmlIgnore]
        public ICanalSection CanalSection { get; set; }
    }
}
