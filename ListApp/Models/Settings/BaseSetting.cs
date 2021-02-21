namespace ListApp.Models.Settings
{
    using ListApp.Enums;

    public abstract class BaseSetting
    {
        public string Title { get; set; }
        public SettingType Type { get; protected set; }
    }
}
