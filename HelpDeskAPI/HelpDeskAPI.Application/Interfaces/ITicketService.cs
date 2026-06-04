using HelpDeskAPI.Core.DTOs;
using HelpDeskAPI.Core.Models;

namespace HelpDeskAPI.Application.Interfaces;

public interface ITicketService
{
    Task<int> CreateTicketAsync(CreateTicketRequest ticketDto);

    Task<TicketSummaryResponse?> GetTicketByIdAsync(int ticketId);
    Task<IEnumerable<TicketSummaryResponse>> GetActiveTicketsAsync();
    Task UpdateTicketStatusAsync(int ticketId, Stato newStatus);

    Task AddCommentToTicketAsync(AddCommentRequest commentDto);
    Task AssignTicketToUserAsync(int ticketId, int userId);
    Task ReopenTicketAsync(int ticketId);
}
