using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Shared.Users;

namespace ScienceAndMaths.Domain
{
    public interface IUserManager
    {
        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="userSignUpInformation"></param>
        void AddUserAccount(UserSignUpInformation userSignUpInformation);

        /// <summary>
        /// Determines if the user already has an account created.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool DoesUserExist(string userName);
    }
}
