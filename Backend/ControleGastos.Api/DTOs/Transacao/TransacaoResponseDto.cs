using ControleGastos.Domain.Enums;

namespace ControleGastos.Api.DTOs.Transacao;

/// <summary>
/// dados de uma transação retornados pela API.
/// </summary>
public class TransacaoResponseDto
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public int PessoaId { get; set; }

    /// <summary>
    /// cria um DTO de resposta a partir da entidade de domínio.
    /// </summary>
    public static TransacaoResponseDto FromEntity(Domain.Entities.Transacao transacao)
    {
        return new TransacaoResponseDto
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Tipo = transacao.Tipo,
            PessoaId = transacao.PessoaId
        };
    }
}