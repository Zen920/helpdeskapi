using HelpDeskAPI.Application.Interfaces;
using HelpDeskAPI.Core.Models;
using HelpDeskAPI.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDeskAPI.Infrastructure.Repositories;

public class AssegnazioneRepository(HelpDeskAPIDbContext _context) : IAssegnazioneRepository
{
    private readonly HelpDeskAPIDbContext _context = _context;
    public async Task<int> Create(Assegnazione entity)
    {
       var e = await _context.Assegnazioni.AddAsync(entity);
       return e.Entity.Id;
    }

    public Task Delete(Assegnazione entity)
    {
        return Task.FromResult(_context.Assegnazioni.Remove(entity));
        
    }

    public async Task<bool> EntityExists(int id)
    {
        return await _context.Assegnazioni.AnyAsync(a => a.Id == id);
    }

    public async Task<ICollection<Assegnazione>> GetAll()
    {
        return await _context.Assegnazioni.AsNoTracking().ToListAsync();
    }

    public Task<Assegnazione?> GetById(int id)
    {
        return Task.FromResult(_context.Assegnazioni.AsNoTracking().FirstOrDefault(a => a.Id == id));
    }

    public Task Update(Assegnazione updatedEntity)
    {
        return Task.FromResult(_context.Assegnazioni.Update(updatedEntity));
    }
}
