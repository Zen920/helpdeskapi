using HelpDeskAPI.Core.Models;

namespace HelpDeskAPI.Core.DTOs;

public record TicketByStatusResponse(int TicketId, string Title, DateTime Date, Stato Status, String User);
