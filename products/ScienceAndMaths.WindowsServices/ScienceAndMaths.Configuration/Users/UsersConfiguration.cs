using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using ScienceAndMaths.Shared;
using ScienceAndMaths.Shared.Users;

namespace ScienceAndMaths.Configuration.Users
{
    [DataContract(Namespace = XmlConsts.XmlNamespace)]
    [Serializable]
    public class UsersConfiguration
    {
        [DataMember]
        public List<UserSignUpInformation> Users { get; set; }
    }
}
