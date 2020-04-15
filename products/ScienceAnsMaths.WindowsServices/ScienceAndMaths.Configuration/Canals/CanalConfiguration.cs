using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Configuration.Canals
{
    [DataContract(Namespace = XmlConsts.XmlNamespace)]
    [Serializable]
    public class CanalConfiguration
    {
        /// <summary>
        /// Represents the configuration identifier.
        /// </summary>
        [DataMember]
        public string ConfigId { get; set; }

        /// <summary>
        /// Represents the arrows of a canal that connect nodes
        /// </summary>
        [DataMember]
        public List<CanalArrow> Arrows { get; set; }

        /// <summary>
        /// Represents the limits of the canal arrows.
        /// </summary>
        [DataMember]
        public List<CanalNode> Nodes { get; set; }
    }
}
