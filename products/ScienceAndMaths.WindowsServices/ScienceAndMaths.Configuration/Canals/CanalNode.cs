using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Configuration.Canals
{
    [XmlInclude(typeof(SluiceGateNode))]
    [DataContract(Namespace = XmlConsts.XmlNamespace)]
    [Serializable]
    public class CanalNode
    {
        [DataMember]
        public string NodeId { get; set; }

        [DataMember]
        public double? WaterLevel { get; set; }
    }
}
