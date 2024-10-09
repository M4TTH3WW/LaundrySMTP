using LaundryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaundryDL;
namespace LaundryBL
{
    public class UserTransactionServices
    {
        UserValidationServices validationServices = new UserValidationServices();
        UserData userData = new UserData();

        public bool CreateUser(User user)
        {
            bool result = false;

            if (validationServices.CheckIfUserExists(user.name, user.clWeight, user.status))
            {
                userData.AddUser(user);
            }

            return result;
        }

        public bool CreateUser(string name, string clWeight, string status)
        {
            User user = new User { name = name, clWeight = clWeight, status = status };

            return CreateUser(user);
        }

        public bool UpdateUser(User user)
        {
            bool result = validationServices.CheckIfNameExists(user.name);

            if (validationServices.CheckIfNameExists(user.name))
            {
                result = userData.UpdateUser(user) > 0;
            }

            return result;
        }

        public bool UpdateUser(string name, string clWeight, string status)
        {
            User user = new User { name = name, clWeight = clWeight, status = status };

            return UpdateUser(user);
        }

        public bool DeleteUser(User user)
        {
            bool result = true;

            if (validationServices.CheckIfNameExists(user.name))
            {
                result = userData.DeleteUser(user) > 0;
            }

            return result;
        }
    }
}