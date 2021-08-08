using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using PayrocTest.Errors;

namespace PayrocTest.Models
{
    public class Url : ValueObject
    {
        public string Value { get; }

        private Url(string value)
        {
            Value = value;
        }

        public static Result<Url> Create(string url)
        {

            return Result.SuccessIf(Uri.IsWellFormedUriString(url, UriKind.Absolute), UrlErrors.NotValidUrl)
                .Map(() => new Url(url));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}