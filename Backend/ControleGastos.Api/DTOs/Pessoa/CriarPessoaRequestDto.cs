namespace ControleGastos.Api.DTOs.Pessoa;

/// <summary>
/// dados recebidos para cadastrar uma nova pessoa.
/// </summary>
public class CriarPessoaRequestDto
{
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
}