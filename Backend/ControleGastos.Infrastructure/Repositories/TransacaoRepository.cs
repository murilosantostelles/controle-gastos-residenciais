using ControleGastos.Application.Interfaces;
using ControleGastos.Domain.Entities;
using ControleGastos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure.Repositories;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly AppDbContext _context;

    public TransacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Transacao> AdicionarAsync(Transacao transacao)
    {
        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();
        return transacao;
    }

    public async Task<List<Transacao>> ListarTodasAsync()
    {
        return await _context.Transacoes.ToListAsync();
    }

    public async Task<Transacao?> ObterPorIdAsync(int id)
    {
        return await _context.Transacoes.FindAsync(id);
    }
}