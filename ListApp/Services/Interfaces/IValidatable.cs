using System.Threading.Tasks;

namespace ListApp.Services
{
    public interface IValidatable
    {
        bool IsValid();
        Task<bool> IsValidAsync();
        bool RunValidation();
        Task<bool> RunValidationAsync();
    }
}
