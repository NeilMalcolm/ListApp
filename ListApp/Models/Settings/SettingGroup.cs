using System.Collections.Generic;

namespace ListApp.Models.Settings
{
    public class SettingGroup : List<BaseSetting>
    {
        public string Title { get; private set; }

        public SettingGroup
        (
            string title, 
            IEnumerable<BaseSetting> settings
        ) : base(settings)
        {
            Title = title;
        }
    }
}
