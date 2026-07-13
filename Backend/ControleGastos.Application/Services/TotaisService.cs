using ControleGastos.Application.Interfaces;
using ControleGastos.Application.Totais;
using ControleGastos.Domain.Enums;

namespace ControleGastos.Application.Services;

public class TotaisService
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ITransacaoRepository _transacaoRepository;

    public TotaisService(
        IPessoaRepository pessoaRepository,
        ITransacaoRepository transacaoRepository)
    {
        _pessoaRepository = pessoaRepository;
        _transacaoRepository = transacaoRepository;
    }

    /// <summary>
    /// calcula o total de receitas, despesas e saldo de cada pessoa,
    /// além do total geral somando todas as pessoas.
    /// </summary>
    public async Task<TotaisResultado> CalcularAsync()
    {
        var pessoas = await _pessoaRepository.ListarTodasAsync();
        var transacoes = await _transacaoRepository.ListarTodasAsync();

        var totaisPorPessoa = pessoas.Select(pessoa =>
        {
            var transacoesDaPessoa = transacoes.Where(t => t.PessoaId == pessoa.Id);

            var receitas = transacoesDaPessoa
                .Where(t => t.Tipo == TipoTransacao.Receita)
                .Sum(t => t.Valor);

            var despesas = transacoesDaPessoa
                .Where(t => t.Tipo == TipoTransacao.Despesa)
                .Sum(t => t.Valor);

            return new TotalPessoa
            {
                PessoaId = pessoa.Id,
                Nome = pessoa.Nome,
                TotalReceitas = receitas,
                TotalDespesas = despesas,
                Saldo = receitas - despesas
            };
        }).ToList();

        return new TotaisResultado
        {
            Pessoas = totaisPorPessoa,
            TotalGeralReceitas = totaisPorPessoa.Sum(p => p.TotalReceitas),
            TotalGeralDespesas = totaisPorPessoa.Sum(p => p.TotalDespesas),
            SaldoGeral = totaisPorPessoa.Sum(p => p.Saldo)
        };
    }
}