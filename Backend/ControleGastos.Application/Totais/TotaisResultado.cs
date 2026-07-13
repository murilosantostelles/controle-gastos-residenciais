namespace ControleGastos.Application.Totais;

/// <summary>
/// resultado completo da consulta de totais: por pessoa e o total geral.
/// </summary>
public class TotaisResultado
{
    public List<TotalPessoa> Pessoas { get; set; } = new();
    public decimal TotalGeralReceitas { get; set; }
    public decimal TotalGeralDespesas { get; set; }
    public decimal SaldoGeral { get; set; }
}