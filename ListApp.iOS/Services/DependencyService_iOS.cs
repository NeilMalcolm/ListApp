using ListApp.DependencyService;
using ListApp.iOS.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(DependencyService_iOS))]
namespace ListApp.iOS.Services
{
    public class DependencyService_iOS : BaseDependencyService
    {
        public override void RegisterNativeDependencies()
        {

        }
    }
}