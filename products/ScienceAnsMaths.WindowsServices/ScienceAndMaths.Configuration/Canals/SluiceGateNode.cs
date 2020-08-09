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
    public class SluiceGateNode : CanalNode
    {
        [DataMember]
        public double GateOpening { get; set; }

        [DataMember]
        public double ContractionCoefficient { get; set; }

        [DataMember]
        public double RetainedWaterLevel { get; set; }
    }
}
