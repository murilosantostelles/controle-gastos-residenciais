using ControleGastos.Application.Totais;

namespace ControleGastos.Api.DTOs.Totais;

/// <summary>
/// totais de receitas, despesas e saldo de uma pessoa específica.
/// </summary>
public class TotalPessoaResponseDto
{
    public int PessoaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal Saldo { get; set; }

    public static TotalPessoaResponseDto FromResult(TotalPessoa total)
    {
        return new TotalPessoaResponseDto
        {
            PessoaId = total.PessoaId,
            Nome = total.Nome,
            TotalReceitas = total.TotalReceitas,
            TotalDespesas = total.TotalDespesas,
            Saldo = total.Saldo
        };
    }
}