using System.ComponentModel.DataAnnotations;

namespace VocabularyApp.Api.ValidationAttributes;

public sealed class IntRangeAttribute : ValidationAttribute
{
    public IntRangeAttribute(int inclusliveMin)
    {
        InclusliveMin = inclusliveMin;
    }

    public IntRangeAttribute(int inclusliveMin, int exclusiveMax)
    {
        InclusliveMin = inclusliveMin;
        ExclusiveMax = exclusiveMax;
    }

    public int InclusliveMin { get; }

    public int? ExclusiveMax { get; }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (int.TryParse(value?.ToString(), out int parsedValue))
        {
            bool rangeValid = parsedValue >= InclusliveMin && (!ExclusiveMax.HasValue || parsedValue < ExclusiveMax);
            if (rangeValid) return ValidationResult.Success;
            else return new ValidationResult(ExclusiveMax.HasValue ? $"Value must be between {InclusliveMin} and {ExclusiveMax.Value}" : $"Value must be greater than or equal to {InclusliveMin}.");
        }

        return ValidationResult.Success;
    }
}
