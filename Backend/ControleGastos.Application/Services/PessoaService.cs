using ControleGastos.Application.Interfaces;
using ControleGastos.Domain.Entities;

namespace ControleGastos.Application.Services;

public class PessoaService
{
    private readonly IPessoaRepository _pessoaRepository;

    public PessoaService(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }

    /// <summary>
    /// Cadastra uma nova pessoa.
    /// </summary>
    public async Task<Pessoa> CriarAsync(string nome, int idade)
    {
        var pessoa = new Pessoa
        {
            Nome= nome,
            Idade= idade
        };

        return await _pessoaRepository.AdicionarAsync(pessoa);
    }

    /// <summary>
    /// Lista todas as pessoas cadastradas.
    /// </summary>
    public async Task<List<Pessoa>> ListarAsync()
    {
        return await _pessoaRepository.ListarTodasAsync();
    }

    /// <summary>
    /// Remove uma pessoa pelo seu ID.
    /// As transações vinculadas a ela também são removidas.
    /// </summary>
    public async Task RemoverAsync(int id)
    {
        var pessoa = await _pessoaRepository.ObterPorIdAsync(id);
        if (pessoa is null)
            throw new KeyNotFoundException("Pessoa não encontrada.");

        await _pessoaRepository.RemoverAsync(pessoa);
    }
}