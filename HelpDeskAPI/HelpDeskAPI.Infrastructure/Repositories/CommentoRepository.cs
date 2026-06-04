using HelpDeskAPI.Application.Interfaces;
using HelpDeskAPI.Core.Models;
using HelpDeskAPI.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDeskAPI.Infrastructure.Repositories;

public class CommentoRepository(HelpDeskAPIDbContext _context) : ICommentoRepository
{
    private readonly HelpDeskAPIDbContext _context = _context;
    public async Task<int> Create(Commento entity)
    {
       var e = await _context.Commenti.AddAsync(entity);
       return e.Entity.Id;
    }

    public Task Delete(Commento entity)
    {
        return Task.FromResult(_context.Commenti.Remove(entity));
        
    }

    public async Task<ICollection<Commento>> GetAll()
    {
        return await _context.Commenti.AsNoTracking().ToListAsync();
    }

    public Task<Commento?> GetById(int id)
    {
        return Task.FromResult(_context.Commenti.AsNoTracking().FirstOrDefault(c => c.Id == id));
    }

    public Task Update(Commento updatedEntity)
    {
        return Task.FromResult(_context.Commenti.Update(updatedEntity));
    }
}
