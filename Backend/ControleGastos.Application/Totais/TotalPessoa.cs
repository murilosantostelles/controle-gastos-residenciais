namespace ControleGastos.Application.Totais;

/// <summary>
/// totais de receitas, despesas e saldo de uma pessoa específica.
/// </summary>
public class TotalPessoa
{
    public int PessoaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal Saldo { get; set; }
}