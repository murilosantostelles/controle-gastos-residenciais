using ControleGastos.Api.DTOs.Totais;
using ControleGastos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TotaisController : ControllerBase
{
    private readonly TotaisService _totaisService;

    public TotaisController(TotaisService totaisService)
    {
        _totaisService = totaisService;
    }

    /// <summary>
    /// retorna o total de receitas, despesas e saldo de cada pessoa,
    /// além do total geral.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<TotaisResponseDto>> Obter()
    {
        var resultado = await _totaisService.CalcularAsync();
        var response = TotaisResponseDto.FromResult(resultado);

        return Ok(response);
    }
}