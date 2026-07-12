using ControleGastos.Domain.Entities;

namespace ControleGastos.Application.Interfaces;

public interface IPessoaRepository
{
    Task<Pessoa> AdicionarAsync(Pessoa pessoa);
    Task<List<Pessoa>> ListarTodasAsync();
    Task<Pessoa?> ObterPorIdAsync(int id);
    Task RemoverAsync(Pessoa pessoa);
}