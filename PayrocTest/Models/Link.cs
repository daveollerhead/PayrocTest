using System;
using System.Text;
using CSharpFunctionalExtensions;
using PayrocTest.Errors;
using PayrocTest.Extensions;

namespace PayrocTest.Models
{
    public class Link : Entity<long>
    {
        public Url Url { get; }

        public Link(Url url)
        {
            Url = url;
        }

        public Result<string> GetShortenedUrl()
        {
            return Result.Success()
                .Ensure(() => Id != 0, LinkErrors.IdNotSet)
                .Map(() => Convert.ToBase64String(Encoding.UTF8.GetBytes(Id.ToString()))
                    .Replace('+', '-')
                    .Replace('/', '_')
                    .TrimEnd('='));
        }

        public static Result<long> GetId(string shortUrl)
        {
            return Result.SuccessIf(() => !string.IsNullOrWhiteSpace(shortUrl), GenericErrors.ParameterIsNull(nameof(shortUrl)))
                .Map(() => shortUrl)
                .Map(x => Encoding.UTF8.GetString(Convert.FromBase64String(x
                    .Replace('-', '+')
                    .Replace('_', '/')
                    .PadBase64())))
                .Ensure(x => long.TryParse(x, out _), LinkErrors.ShortUrlCouldNotBeParsed)
                .Map(long.Parse);
        }
    }
}