using System;
using System.ComponentModel.DataAnnotations;
using PayrocTest.Models;

namespace PayrocTest.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidUrlAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            const string errorMessage = "Please enter a valid URL";

            if (value == null)
                return ValidationResult.Success;

            var url = value as string;
            if (url == null)
                return new ValidationResult(errorMessage);

            var urlResult = Url.Create(url);
            if (urlResult.IsFailure)
                return new ValidationResult(errorMessage);

            return ValidationResult.Success;
        }
    }
}
