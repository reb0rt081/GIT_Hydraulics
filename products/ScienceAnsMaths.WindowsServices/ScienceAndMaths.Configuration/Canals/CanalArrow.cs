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
        public ICanalSection CanalSection { get; set; }
    }
}
