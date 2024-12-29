
using Rira_TohidBadri_Crud.Contract.Errors;

namespace Rira_TohidBadri_Crud.Contract.Exceptions;

public class CustomValidationException : Exception
{
    public CustomValidationException(List<ValidatorError> validatorErrors)
    {
        this.ValidatorErrors = validatorErrors;
    }

    public List<ValidatorError> ValidatorErrors { get; set; }
}
