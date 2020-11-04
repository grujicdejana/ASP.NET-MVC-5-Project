using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VidlyProject.Models
{
    public class NumberInStockCustomValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var movie = (Movie)validationContext.ObjectInstance;

            var numberInStock = movie.NumberInStock;

            return (numberInStock > 0 && numberInStock <= 20)
                ? ValidationResult.Success
                : new ValidationResult("The field Number in Stock must be between 1 and 20");
        }
    }
}