namespace ListApp.Models.Settings
{
    using ListApp.Enums;
    using System.Windows.Input;

    public class ActionSetting : BaseSetting
    {
        public ICommand OnActionCommand { get; set; }

        public ActionSetting()
        {
            Type = SettingType.Action;
        }
    }
}
