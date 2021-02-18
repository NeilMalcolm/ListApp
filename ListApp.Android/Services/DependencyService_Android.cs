using ListApp.DependencyService;
using ListApp.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(DependencyService_Android))]
namespace ListApp.Droid.Services
{
    public class DependencyService_Android : BaseDependencyService
    {
        public override void RegisterNativeDependencies()
        {

        }
    }
}