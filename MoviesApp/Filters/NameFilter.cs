using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MoviesApp.ViewModels;

namespace MoviesApp.Filters
{
    public class NameFilter : ValidationAttribute
    {
        public NameFilter()
        {
        }
        public string GetErrorMessage() =>
            $"больше 4х знаков э слыыыш";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = (string)value;
            if (str.Length < 4)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}