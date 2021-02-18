using ListApp.Models.CrudModels;
using System;

namespace ListApp.Models
{
    public class User : BaseRecordableRecord
    {
        public string Name { get; set; }
        public string ProfileImageBytes { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.UtcNow;
        }

        public User(UserDetailModel userDetailModel) 
            :this ()
        {
            if (userDetailModel is null)
            {
                throw new InvalidOperationException("User Detail Model should not be null");
            }

            Name = userDetailModel.Name;
            if (userDetailModel.HasImage)
            {
                ProfileImageBytes = userDetailModel.ImageBytes;
            }
        }
    }
}
