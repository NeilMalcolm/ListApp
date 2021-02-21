using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace ListApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseModalPage : BasePage
    {
        Button closeButton;

        public BaseModalPage()
        {
            InitializeComponent();
        }


        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            closeButton = (Button)GetTemplateChild("CloseButton");
        }
        protected override bool OnBackButtonPressed()
        {
            if (closeButton?.Command != null)
            {
                closeButton.Command.Execute(null);
            }

            return true;
        }

    }
}