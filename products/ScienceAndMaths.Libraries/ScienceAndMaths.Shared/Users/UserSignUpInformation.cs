using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Shared.Users
{
    [DataContract(Namespace = XmlConsts.XmlNamespace)]
    [Serializable]
    public class UserSignUpInformation
    {
        [DataMember]
        [Key]
        public int UserId { get; set; }

        [DataMember]
        public int RoleId { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "*")]
        [Display(Name = "Login name")]
        public string Name { get; set; }

        [DataMember]
        [Required(ErrorMessage = "*")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataMember]
        [Required(ErrorMessage = "*")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "*")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
