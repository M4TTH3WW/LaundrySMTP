using LaundryModel;
namespace LaundryDL
{
    public class UserData
    {
        List<User> users;
        SqlDbData sqlData;

        public UserData()
        {
            users = new List<User>();
            sqlData = new SqlDbData();
        }
        public List<User> GetUsers()
        {
            users = sqlData.GetUsers();
            return users;
        }

        public int AddUser(User user)
        {
            return sqlData.AddUser(user.name, user.clWeight, user.status);
        }

        public int UpdateUser(User user)
        {
            return sqlData.UpdateUser(user.name, user.clWeight, user.status);
        }

        public int DeleteUser(User user)
        {
            return sqlData.DeleteUser(user.name);
        }
    }
}