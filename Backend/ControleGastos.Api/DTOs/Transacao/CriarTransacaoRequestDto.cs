using System.ComponentModel.DataAnnotations;
using ControleGastos.Domain.Enums;

namespace ControleGastos.Api.DTOs.Transacao;

public class CriarTransacaoRequestDto
{
    [Required(ErrorMessage = "descrição é obrigatória.")]
    [MaxLength(255, ErrorMessage = "descrição deve ter até 255 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "valor deve ser maior que zero.")]
    public decimal Valor { get; set; }

    public TipoTransacao Tipo { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "PessoaId é obrigatório.")]
    public int PessoaId { get; set; }
}