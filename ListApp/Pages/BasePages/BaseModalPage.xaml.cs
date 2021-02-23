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
        }
    }
}