using ControleGastos.Domain.Entities;

namespace ControleGastos.Api.DTOs.Pessoa;

/// <summary>
/// dados de uma pessoa retornados pela API.
/// </summary>
public class PessoaResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }

    /// <summary>
    /// cria um DTO de resposta a partir da entidade de domínio.
    /// </summary>
    public static PessoaResponseDto FromEntity(Domain.Entities.Pessoa pessoa)
    {
        return new PessoaResponseDto
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Idade = pessoa.Idade
        };
    }
}