using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface IStorageProvider
    {
        Task<string> ReadAsync(string fileName);
        Task SaveAsync(string fileName, string content);
        Task DeleteAsync(string fileName);
    }
}