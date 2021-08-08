using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PayrocTest.Data;
using PayrocTest.Errors;
using PayrocTest.Models;

namespace PayrocTest.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> GetLongUrl(string shortUrl)
        {
            return await Link.GetId(shortUrl)
                .Map(async x => await _unitOfWork.GetRepository<Link, long>().Find(x))
                .Ensure(x => x != null, GenericErrors.EntityNotFound(nameof(Link)))
                .Map(x => x.Url.Value);
        }

        public async Task<Result<string>> CreateShortUrl(string longUrl)
        {
            return await Url.Create(longUrl)
                .Map(x => new Link(x))
                .Tap(x => _unitOfWork.GetRepository<Link, long>().Add(x))
                .Tap(async () => await _unitOfWork.Commit())
                .Bind(x => x.GetShortenedUrl());
        }
    }
}