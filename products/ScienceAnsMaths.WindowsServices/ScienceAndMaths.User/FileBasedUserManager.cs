using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

using ScienceAndMaths.Configuration.Users;
using ScienceAndMaths.Domain;
using ScienceAndMaths.Shared.Users;

namespace ScienceAndMaths.User
{
    public class FileBasedUserManager : IUserManager
    {
        public const string FileName = "UsersConfiguration.xml";

        public readonly string FilePath;

        public FileBasedUserManager(string filePath)
        {
            FilePath = filePath;
        }

        public void AddUserAccount(UserSignUpInformation userSignUpInformation)
        {
            throw new NotImplementedException();
        }

        public bool DoesUserExist(string userName)
        {
            throw new NotImplementedException();
        }

        private List<UserSignUpInformation> GetAllUsers()
        {
            string fileContent;

            using (StreamReader streamReader = File.OpenText(Path.Combine(FilePath, FileName)))
            {
                fileContent = streamReader.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(fileContent))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(UsersConfiguration));
                UsersConfiguration deserializedConfiguration;

                using (StringReader sr = new StringReader(fileContent))
                {
                    deserializedConfiguration = (UsersConfiguration)xmlSerializer.Deserialize(sr);
                }

                return deserializedConfiguration.Users;
            }

            return null;
        }

        private void SaveUser(UserSignUpInformation userInformation)
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            string xml;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(UsersConfiguration));

            var allUsers = GetAllUsers();

            allUsers.Add(userInformation);

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, allUsers);
                    xml = sww.ToString(); // Your XML
                }

                File.WriteAllText(Path.Combine(FilePath, FileName), xml);
            }
        }
    }
}
