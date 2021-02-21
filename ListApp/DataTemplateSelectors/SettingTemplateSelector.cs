using ListApp.Models.Settings;
using ListApp.Views.DataTemplates;
using System;
using Xamarin.Forms;

namespace ListApp.DataTemplateSelectors
{
    public class SettingTemplateSelector : DataTemplateSelector
    {
        public SettingActionDataTemplate ActionDataTemplate = new SettingActionDataTemplate();
        public SettingRadioDataTemplate RadioDataTemplate = new SettingRadioDataTemplate();

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DataTemplate template = null;

            if (item is BaseSetting baseSetting)
            {
                template = baseSetting.Type switch
                {
                    Enums.SettingType.Radio => RadioDataTemplate,
                    Enums.SettingType.Action => ActionDataTemplate,
                    _ => throw new InvalidOperationException($"Must declare template for {nameof(BaseSetting)} with Type {baseSetting.Type}"),
                };
            }

            if (template is null)
            {
                throw new InvalidOperationException($"Data must be type of {nameof(BaseSetting)}");
            }

            return template;
        }
    }
}
