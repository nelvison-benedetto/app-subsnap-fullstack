using FluentValidation;
using FluentValidation.Results;

namespace SubSnap.API.Validators;

public static class ValidatorHelper
{
    /// <summary>
    /// Valida un command/DTO usando FluentValidation.
    /// Se ci sono errori, lancia un ValidationException.
    /// </summary>
    public static void ValidateCommand<T>(IValidator<T> validator, T command)
    {
        ValidationResult result = validator.Validate(command);

        if (!result.IsValid)
        {
            // Puoi formattare gli errori come vuoi
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            // Lancia una ValidationException FluentValidation oppure usa la tua ApiError
            throw new FluentValidation.ValidationException(result.Errors);
        }
    }

    /// <summary>
    /// Versione async
    /// </summary>
    public static async Task ValidateCommandAsync<T>(IValidator<T> validator, T command)
    {
        ValidationResult result = await validator.ValidateAsync(command);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new FluentValidation.ValidationException(result.Errors);
        }
    }
}
