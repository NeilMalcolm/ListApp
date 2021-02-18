using System.IO;
using System.Threading.Tasks;

namespace ListApp.Services
{
    public interface ISerializer
    {
        T Deserialize<T>(string content) where T : new();

        Task<T> DeserializeAsync<T>(Stream content) where T : new();

        string Serialize<T>(T objectToSerialize) where T : new();
    }
}
