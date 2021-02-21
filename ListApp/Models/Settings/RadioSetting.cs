namespace ListApp.Models.Settings
{
    using ListApp.Enums;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;

    public class RadioSetting : BaseSetting
    {
        private object selectedOption;
        public object SelectedOption
        { 
            get => selectedOption; 
            set
            {
                if (value != null && value != selectedOption)
                {
                    OnSelectionChangedCommand?.Execute(value);
                }
                selectedOption = value;
            }
        }
        public ICommand OnSelectionChangedCommand { get; set; }

        public RadioOption[] Options { get; set; }

        public RadioSetting()
        {
            Type = SettingType.Radio;
        }
    }

    public class RadioOption
    {
        public string Title { get; set; }
        public bool IsChecked { get; set; }
    }
}
