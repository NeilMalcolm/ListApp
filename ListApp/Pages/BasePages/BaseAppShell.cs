using ListApp.Helpers.PageHelpers;

namespace ListApp.Pages
{
    public abstract class BaseAppShell : Xamarin.Forms.Shell
    {
        public BaseAppShell()
        {
            this.LoadDataOnAppearing().Wait();
        }
    }
}
