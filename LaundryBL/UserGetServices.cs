using LaundryModel;
using LaundryDL;
namespace LaundryBL
{
    public class UserGetServices
    {
        public List<User> GetAllUsers()
        {
            UserData userData = new UserData();

            return userData.GetUsers();

        }

        public List<User> GetUsers(string name)
        {
            List<User> users = new List<User>();

            foreach (var user in GetAllUsers())
            {
                if (user.status == name)
                {
                    users.Add(user);
                }
            }

            return users;
        }

        public User GetUsers(string name, string clWeight, string status)
        {
            User foundUser = new User();

            foreach (var user in GetAllUsers())
            {
                if (user.name == name && user.clWeight == clWeight && user.status == status)

                {
                    foundUser = user;
                }
            }

            return foundUser;
        }
    }
}