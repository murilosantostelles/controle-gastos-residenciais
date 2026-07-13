namespace ControleGastos.Api.DTOs.Pessoa;
using System.ComponentModel.DataAnnotations;


/// <summary>
/// dados recebidos para cadastrar uma nova pessoa.
/// </summary>
public class CriarPessoaRequestDto
{
    [Required(ErrorMessage = "nome é obrigatório.")]
    [MaxLength(50, ErrorMessage = "nome deve ter até 50 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Range(0, 120, ErrorMessage = "idade deve estar entre 0 e 120.")]
    public int Idade { get; set; }
}