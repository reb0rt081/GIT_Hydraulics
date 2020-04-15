using System;
using System.Runtime.Serialization;
using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Data.Canals
{
    [DataContract(Namespace = XmlConsts.XmlNamespace)]
    [Serializable]
    public class CanalNode
    {
        [DataMember]
        public string NodeId { get; set; }
    }
}
