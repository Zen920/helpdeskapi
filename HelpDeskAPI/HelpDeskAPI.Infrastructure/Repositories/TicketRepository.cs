using HelpDeskAPI.Application.Interfaces;
using HelpDeskAPI.Core.Models;
using HelpDeskAPI.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDeskAPI.Infrastructure.Repositories;

public class TicketRepository(HelpDeskAPIDbContext _context) : ITicketRepository
{
    private readonly HelpDeskAPIDbContext _context = _context;
    public async Task<int> Create(Ticket entity)
    {
       var e = await _context.Tickets.AddAsync(entity);
       return e.Entity.Id;
    }

    public Task Delete(Ticket entity)
    {
        return Task.FromResult(_context.Tickets.Remove(entity));
        
    }

    public async Task<ICollection<Ticket>> GetAll()
    {
        return await _context.Tickets.AsNoTracking().ToListAsync();
    }

    public Task<Ticket?> GetById(int id)
    {
        return Task.FromResult(_context.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == id));
    }

    public Task Update(Ticket updatedEntity)
    {
        return Task.FromResult(_context.Tickets.Update(updatedEntity));
    }
}
