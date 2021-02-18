using ListApp.Models;
using ListApp.Models.CrudModels;
using System;
using System.Threading.Tasks;

namespace ListApp.Services
{
    public interface IUserLogic
    {
        User CurrentUser { get; }
        Task<bool> DoesUserExist();
        Task<User> Get();
        Task<bool> CreateUser(UserDetailModel userDetails);
        Task<bool> DeleteUser();
        Task<bool> UpdateUserDetails(UserDetailModel updateDetails);
    }
}
