using HelpDeskAPI.Core.Models;

namespace HelpDeskAPI.Core.DTOs;

public record CreateTicketRequest(int UtenteId, string Titolo, string Descrizione, Priorità Priority, int EffortRequired, DateTime Date);

