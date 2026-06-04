using HelpDeskAPI.Application.Interfaces;
using HelpDeskAPI.Core.DTOs;
using HelpDeskAPI.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDeskAPI.Infrastructure.Repositories;

public class TicketReadRepository(HelpDeskAPIDbContext _context) : ITicketReadRepo
{
    private readonly HelpDeskAPIDbContext _context = _context;
    public async Task<ICollection<TicketByStatusResponse>> GetActiveTickets()
    {
        return await _context.Tickets
                .AsNoTracking()
                .Where(t => t.Stato != Core.Models.Stato.CHIUSO)
                .Select(t => new TicketByStatusResponse(t.Id, t.Titolo, t.Date,t.Stato,t.Utente.Nome ))
                .ToListAsync();

    }
}
