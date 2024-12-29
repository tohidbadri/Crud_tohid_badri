using Grpc.Core;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Rira_TohidBadri_Crud.Contract.Exceptions;

namespace Rira_TohidBadri_Crud.GRPC.Handlers
{
    public class ExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = CreateProblemDetails(exception);
            httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            if (exception is CustomValidationException customValidationException)
            {
                var rpcException = HandleValidationException(customValidationException);
                
            }
            else if (exception is Exception)
            {
                var rpcException = HandleInternalException(exception);
                
            }

            return true;
        }

        private static ProblemDetails CreateProblemDetails(Exception exception)
        {
            ProblemDetails problemDetails = exception switch
            {
                NotFoundException => CreateProblemDetails(StatusCodes.Status404NotFound, "Not Found", exception.Message),
                CustomValidationException => CreateProblemDetails(StatusCodes.Status400BadRequest, "Validation Error", "One or more validation errors occurred"),
                _ => CreateProblemDetails(StatusCodes.Status500InternalServerError, "Internal Server Error", "An unexpected error occurred")
            };

            if (exception is CustomValidationException customValidationException)
            {
                problemDetails.Extensions["errors"] = customValidationException.ValidatorErrors
                    .Select(e => new
                    {
                        Property = e.Property,
                        ErrorMessage = e.ErrorMessage
                    });
            }

            return problemDetails;
        }

        private static ProblemDetails CreateProblemDetails(int status, string title, string detail)
        {
            return new ProblemDetails { Status = status, Title = title, Detail = detail };
        }

        public static RpcException HandleValidationException(CustomValidationException ex)
        {
            return new RpcException(
                new Status(StatusCode.InvalidArgument, "Validation Error"),
                new Metadata
                {
                    { "validation-errors", string.Join("; ", ex.ValidatorErrors.Select(e => $"{e.Property}: {e.ErrorMessage}")) }
                });
        }

        public static RpcException HandleInternalException(Exception ex)
        {
            return new RpcException(
                new Status(StatusCode.Internal, "Internal Server Error"),
                new Metadata
                {
                    { "exception-errors", ex.Message }
                });
        }
    }
}
