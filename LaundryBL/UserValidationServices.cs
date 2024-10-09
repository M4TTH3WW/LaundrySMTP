using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaundryDL;
namespace LaundryBL
{
    public class UserValidationServices
    {

        UserGetServices getservices = new UserGetServices();

        public bool CheckIfNameExists(string name)
        {
            bool result = getservices.GetUsers(name) != null;
            return result;
        }

        public bool CheckIfUserExists(string name, string clWeight, string status)
        {
            bool result = getservices.GetUsers(name, clWeight, status) != null;
            return result;
        }

    }
}