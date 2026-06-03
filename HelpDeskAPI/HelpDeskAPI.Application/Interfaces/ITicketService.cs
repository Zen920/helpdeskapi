using HelpDeskAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDeskAPI.Application.Interfaces;

public interface ITicketService
{
    Task<int> CreateTicketAsync(CreateTicketDto ticketDto);

    Task<TicketDto?> GetTicketByIdAsync(int ticketId);
    Task<IEnumerable<TicketSummaryDto>> GetActiveTicketsAsync();
    Task UpdateTicketStatusAsync(int ticketId, TicketStatus newStatus);

    Task AddCommentToTicketAsync(int ticketId, AddCommentDto commentDto);
    Task AssignTicketToUserAsync(int ticketId, int userId);

}
