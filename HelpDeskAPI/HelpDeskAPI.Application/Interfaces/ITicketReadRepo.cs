using HelpDeskAPI.Core.DTOs;
namespace HelpDeskAPI.Application.Interfaces;

public interface ITicketReadRepo
{
    Task<ICollection<TicketByStatusResponse>> GetActiveTickets();
}
