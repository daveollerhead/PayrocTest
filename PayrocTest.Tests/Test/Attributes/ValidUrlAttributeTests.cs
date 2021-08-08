using PayrocTest.Attributes;
using Xunit;

namespace PayrocTest.Tests.Test.Attributes
{
    public class ValidUrlAttributeTests
    {
        [Fact]
        public void IsValid_ValueIsNull_ReturnsTrue()
        {
            var att = new ValidUrlAttribute();

            var result = att.IsValid(null);

            Assert.True(result);
        }

        [Fact]
        public void IsValid_ValueIsNotValidString_ReturnsFalse()
        {
            var att = new ValidUrlAttribute();

            var result = att.IsValid(1);

            Assert.False(result);
        }

        [Fact]
        public void IsValid_ValueIsNotValidUrl_ReturnsFalse()
        {
            var att = new ValidUrlAttribute();

            var result = att.IsValid("123");

            Assert.False(result);
        }

        [Fact]
        public void IsValid_ValueIsValidUrl_ReturnsTrue()
        {
            var att = new ValidUrlAttribute();

            var result = att.IsValid("https://google.com");

            Assert.True(result);
        }
    }
}
