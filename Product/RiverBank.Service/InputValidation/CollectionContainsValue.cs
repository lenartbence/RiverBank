using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RiverBank.Service.InputValidation
{
    /// <summary>
    /// Attribute to validate if a value is part of a given set of values.
    /// </summary>
    public class CollectionContainsValue : ValidationAttribute
    {
        public CollectionContainsValue(object[] collection)
        {
            Collection = collection ?? new object[] { };
        }

        public object[] Collection { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (Collection.Contains(value))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}