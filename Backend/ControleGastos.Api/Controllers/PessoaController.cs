using ControleGastos.Api.DTOs.Pessoa;
using ControleGastos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    private readonly PessoaService _pessoaService;

    public PessoaController(PessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }

    public async Task<ActionResult<PessoaResponseDto>> Criar(CriarPessoaRequestDto dto)
    {
        var pessoa = await _pessoaService.CriarAsync(dto.Nome, dto.Idade);
        var response = PessoaResponseDto.FromEntity(pessoa);

        return CreatedAtAction(nameof(Criar), new {id = pessoa.Id}, dto);
    }


    /// <summary>
    /// lista todas as pessoas cadastradas.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<PessoaResponseDto>>> Listar()
    {
        var pessoas = await _pessoaService.ListarAsync();
        var response = pessoas.Select(PessoaResponseDto.FromEntity).ToList();

        return Ok(response);
    }

    /// <summary>
    /// remove uma pessoa e todas as suas transações.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        await _pessoaService.RemoverAsync(id);
        return NoContent();
    }
}