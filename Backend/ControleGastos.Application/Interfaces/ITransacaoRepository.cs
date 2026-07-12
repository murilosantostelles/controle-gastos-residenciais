using ControleGastos.Domain.Entities;

namespace ControleGastos.Application.Interfaces;

public interface ITransacaoRepository
{
    Task<Transacao> AdicionarAsync(Transacao transacao);
    Task<List<Transacao>> ListarTodasAsync();
    Task<Transacao?> ObterPorIdAsync(int id);
}