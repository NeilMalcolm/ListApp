using System;
using System.Collections.Generic;
using System.Text;

namespace ListApp.Models.CrudModels
{
    public class UserDetailModel
    {
        public string Name { get; set; }
        public string ImageBytes { get; set; }
        public bool HasImage => string.IsNullOrWhiteSpace(ImageBytes);
        public bool IsNameValid => !string.IsNullOrWhiteSpace(ImageBytes);
    }
}
