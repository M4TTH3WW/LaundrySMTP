using Microsoft.AspNetCore.Mvc;
using LaundryModel;
using LaundryBL;

namespace LaundryAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        UserGetServices _userGetServices;
        UserTransactionServices _userTransactionServices;

        public UserController()
        {
            _userGetServices = new UserGetServices();
            _userTransactionServices = new UserTransactionServices();
        }

        [HttpGet]

        public IEnumerable<User> GetUsers()
        {
            var user = _userGetServices.GetAllUsers();

            List<User> users = new List<User>();

            foreach (var Users in user)
            {
                users.Add(new LaundryAPI.User { name = Users.name, clWeight = Users.clWeight, status = Users.status });
            }
            return users;
        }

        [HttpPost]
        public JsonResult AddUser(User request)
        {
            var result = _userTransactionServices.CreateUser(request.name, request.clWeight, request.status);

            return new JsonResult(result);
        }

        [HttpPatch]
        public JsonResult UpdateUSer(User request)
        {
            var result = _userTransactionServices.UpdateUser(request.name, request.clWeight, request.status);

            return new JsonResult(result);
        }

        [HttpDelete]
        public JsonResult DeleteUser(User request)
        {
            var deleteCustomer = new LaundryModel.User
            {
                name = request.name
            };

            var result = _userTransactionServices.DeleteUser(deleteCustomer);

            return new JsonResult(result);
        }


    }
}
