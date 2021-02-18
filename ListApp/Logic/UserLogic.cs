using ListApp.Enums;
using ListApp.Models;
using ListApp.Models.CrudModels;
using System.Threading.Tasks;

namespace ListApp.Services
{
    public class UserLogic : IUserLogic
    {
        private readonly IDatabaseService _databaseService;
        private readonly ILog _log;

        public UserLogic(IDatabaseService databaseService,
            ILog log)
        {
            _databaseService = databaseService;
            _log = log;
        }

        public User CurrentUser { get; private set; }

        public async Task<bool> CreateUser(UserDetailModel userDetails)
        {
            if (userDetails is null)
            {
                return false;
            }

            var newUser = new User(userDetails);
            return ReturnCrudResultToBool(await _databaseService.InsertAsync(newUser));
        }

        public async Task<bool> DeleteUser()
        {
            await Get();

            if (CurrentUser is null)
            {
                return false;
            }

            await _databaseService.DeleteAsync(CurrentUser);
            CurrentUser = null;
            return true;
        }

        public async Task<bool> DoesUserExist()
        {
            if (CurrentUser != null)
            {
                return true;
            }

            await Get();
            return CurrentUser != null;
        }

        public async Task<User> Get()
        {
            if (CurrentUser != null)
            {
                return CurrentUser;
            }

            return CurrentUser = await _databaseService.FirstAsync<User>();
        }

        public async Task<bool> UpdateUserDetails(UserDetailModel updateDetails)
        {
            await Get();

            if (CurrentUser is null)
            {
                return false;
            }

            if (updateDetails.IsNameValid)
            {
                CurrentUser.Name = updateDetails.Name;
            }
            if (!updateDetails.HasImage)
            {
                CurrentUser.ProfileImageBytes = null;
            }

            return ReturnCrudResultToBool(await _databaseService.UpdateAsync(CurrentUser));
        }

        private bool ReturnCrudResultToBool(CrudResult result)
        {
            return result == CrudResult.Success;
        }
    }
}
