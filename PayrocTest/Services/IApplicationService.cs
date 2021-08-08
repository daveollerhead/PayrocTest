using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace PayrocTest.Services
{
    public interface IApplicationService
    {
        Task<Result<string>> GetLongUrl(string shortUrl);
        Task<Result<string>> CreateShortUrl(string longUrl);
    }
}