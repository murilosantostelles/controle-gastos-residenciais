using ControleGastos.Domain.Enums;

namespace ControleGastos.Api.DTOs.Transacao;

/// <summary>
/// dados recebidos para cadastrar uma nova transação.
/// </summary>
public class CriarTransacaoRequestDto
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public int PessoaId { get; set; }
}