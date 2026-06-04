using HelpDeskAPI.Application.Interfaces;
using HelpDeskAPI.Core.Models;
using HelpDeskAPI.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDeskAPI.Infrastructure.Repositories;

public class UtenteRepository(HelpDeskAPIDbContext _context) : IUtenteRepository
{
    private readonly HelpDeskAPIDbContext _context = _context;
    public async Task<int> Create(Utente entity)
    {
       var e = await _context.Utenti.AddAsync(entity);
       return e.Entity.Id;
    }

    public async Task<bool> EntityExists(int id)
    {
        return await _context.Utenti.AnyAsync(u => u.Id == id);
    }
    public Task Delete(Utente entity)
    {
        return Task.FromResult(_context.Utenti.Remove(entity));
        
    }

    public async Task<ICollection<Utente>> GetAll()
    {
        return await _context.Utenti.AsNoTracking().ToListAsync();
    }

    public Task<Utente?> GetById(int id)
    {
        return Task.FromResult(_context.Utenti.AsNoTracking().FirstOrDefault(u => u.Id == id));
    }

    public Task Update(Utente updatedEntity)
    {
        return Task.FromResult(_context.Utenti.Update(updatedEntity));
    }
}
