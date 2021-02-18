using Xamarin.Forms.Xaml;

namespace ListApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemPage : BasePage
    {
        public ItemPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            if (CloseButton?.Command != null)
            {
                CloseButton.Command.Execute(null);
            }

            return true;
        }

    }
}