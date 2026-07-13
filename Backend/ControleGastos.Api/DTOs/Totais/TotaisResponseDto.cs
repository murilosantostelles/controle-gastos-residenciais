using ControleGastos.Application.Totais;

namespace ControleGastos.Api.DTOs.Totais;

/// <summary>
/// resultado completo da consulta de totais -> por pessoa e o total geral.
/// </summary>
public class TotaisResponseDto
{
    public List<TotalPessoaResponseDto> Pessoas { get; set; } = new();
    public decimal TotalGeralReceitas { get; set; }
    public decimal TotalGeralDespesas { get; set; }
    public decimal SaldoGeral { get; set; }

    public static TotaisResponseDto FromResult(TotaisResultado resultado)
    {
        return new TotaisResponseDto
        {
            Pessoas = resultado.Pessoas.Select(TotalPessoaResponseDto.FromResult).ToList(),
            TotalGeralReceitas = resultado.TotalGeralReceitas,
            TotalGeralDespesas = resultado.TotalGeralDespesas,
            SaldoGeral = resultado.SaldoGeral
        };
    }
}