using HelpDeskAPI.Core.DTOs;
using HelpDeskAPI.Core.Models;

namespace HelpDeskAPI.Application.Interfaces;

public interface ITicketService
{
    Task<int> CreateTicketAsync(CreateTicketRequest ticketDto);

    Task<TicketSummaryResponse?> GetTicketByIdAsync(int ticketId);
    Task<ICollection<TicketByStatusResponse>> GetActiveTicketsAsync();
    Task UpdateTicketStatusAsync(UpdateTicketRequest request);

    Task AddCommentToTicketAsync(AddCommentRequest commentDto);
    Task AssignTicketToUserAsync(AssignTicketToUserRequest request);
    Task ReopenTicketAsync(int ticketId);
    Task<ICollection<CommentoSummaryResponse>> GetCommentsOfTicket(int tickedId);
}
