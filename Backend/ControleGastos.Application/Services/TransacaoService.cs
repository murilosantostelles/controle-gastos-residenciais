using ControleGastos.Application.Interfaces;
using ControleGastos.Domain.Entities;
using ControleGastos.Domain.Enums;

namespace ControleGastos.Application.Services;

public class TransacaoService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IPessoaRepository _pessoaRepository;

    public TransacaoService(ITransacaoRepository transacaoRepository, IPessoaRepository pessoaRepository)
    {
        _transacaoRepository = transacaoRepository;
        _pessoaRepository = pessoaRepository;
    }


    /// <summary>
    /// cadastra uma nova transação para uma pessoa.
    /// a pessoa informada precisa existir
    /// pessoas menores de 18 anos só podem cadastrar despesas.
    /// </summary>
    public async Task<Transacao> CriarAsync(string descricao, decimal valor, TipoTransacao tipo, int pessoaId)
    {
        var pessoa = await _pessoaRepository.ObterPorIdAsync(pessoaId);

        if (pessoa is null)
            throw new KeyNotFoundException("Pessoa não encontrada.");

        if (pessoa.Idade < 18 && tipo == TipoTransacao.Receita)
            throw new InvalidOperationException("Pessoas menores de 18 anos só podem cadastrar despesas.");

        var transacao = new Transacao
        {
            Descricao = descricao,
            Valor = valor,
            Tipo = tipo,
            PessoaId = pessoaId
        };

        return await _transacaoRepository.AdicionarAsync(transacao);
    }

    public async Task<List<Transacao>> ListasAsync()
    {
        return await _transacaoRepository.ListarTodasAsync();
    }

}