using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Middlewares;

/// <summary>
/// centraliza o tratamento de exceções da api, convertendo exceções
/// de negócio em respostas com status http apropriado.
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, mensagem) = exception switch
        {
            KeyNotFoundException => (StatusCodes.Status404NotFound, exception.Message),
            InvalidOperationException => (StatusCodes.Status400BadRequest, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado.")
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = statusCode,
            Title = mensagem
        }, cancellationToken);

        return true;
    }
}