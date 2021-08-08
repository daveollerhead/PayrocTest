using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Xunit;

namespace PayrocTest.Tests.Helpers
{
    public static class ResultExtensions
    {
        public static void AssertFailed<T>(this Result<T> source, string expectedErrorMessage)
        {
            Assert.True(source.IsFailure, "Result was successful");
            Assert.Equal(expectedErrorMessage, source.Error);
        }

        public static void AssertSucceeded<T>(this Result<T> source)
        {
            Assert.True(source.IsSuccess, source.IsFailure ? source.Error : "");

        }
    }
}
