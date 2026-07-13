using ControleGastos.Application.Interfaces;
using ControleGastos.Domain.Entities;
using ControleGastos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly AppDbContext _context;

    public PessoaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Pessoa> AdicionarAsync(Pessoa pessoa)
    {
        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();
        return pessoa;
    }

    public async Task<List<Pessoa>> ListarTodasAsync()
    {
        return await _context.Pessoas.ToListAsync();
    }

    public async Task<Pessoa?> ObterPorIdAsync(int id)
    {
        return await _context.Pessoas.FindAsync(id);
    }

    public async Task RemoverAsync(Pessoa pessoa)
    {
        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();
    }
}