using PayrocTest.Extensions;
using Xunit;

namespace PayrocTest.Tests.Test.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("1", "1===")]
        [InlineData("12", "12==")]
        [InlineData("123", "123=")]
        [InlineData("1234", "1234")]
        [InlineData("12345", "12345===")]
        public void PadBase64_WhenCalled_PadsRightWithEqualsUntilStringLengthIsDivisibleByFour(string input, string output)
        {
            Assert.Equal(output, input.PadBase64());            
        }
    }
}
