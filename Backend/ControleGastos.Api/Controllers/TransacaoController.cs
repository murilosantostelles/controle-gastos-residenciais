using ControleGastos.Api.DTOs.Transacao;
using ControleGastos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacaoController : ControllerBase
{
    private readonly TransacaoService _transacaoService;

    public TransacaoController(TransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

    /// <summary>
    /// cadastra uma nova transação para uma pessoa.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TransacaoResponseDto>> Criar(CriarTransacaoRequestDto request)
    {
        var transacao = await _transacaoService.CriarAsync(
            request.Descricao, request.Valor, request.Tipo, request.PessoaId);

        var response = TransacaoResponseDto.FromEntity(transacao);

        return CreatedAtAction(nameof(Criar), new { id = transacao.Id }, response);
    }

    /// <summary>
    /// lista todas as transações cadastradas.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<TransacaoResponseDto>>> Listar()
    {
        var transacoes = await _transacaoService.ListarAsync();
        var response = transacoes.Select(TransacaoResponseDto.FromEntity).ToList();

        return Ok(response);
    }
}