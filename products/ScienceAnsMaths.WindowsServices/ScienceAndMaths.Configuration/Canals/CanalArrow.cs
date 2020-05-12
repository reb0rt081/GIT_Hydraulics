using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using ScienceAndMaths.Shared;
using ScienceAndMaths.Shared.Canals;

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
        public RectangularSectionConfiguration RectangularSection
        {
            get
            {
                return CanalSection as RectangularSectionConfiguration;
            }
            set
            {
                CanalSection = value;
            }
        }

        [DataMember]
        public TrapezoidalSectionConfiguration TrapezoidalSection
        {
            get
            {
                return CanalSection as TrapezoidalSectionConfiguration;
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
