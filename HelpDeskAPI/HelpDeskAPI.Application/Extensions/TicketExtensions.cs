using HelpDeskAPI.Core.DTOs;
using HelpDeskAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDeskAPI.Application.Extensions;

public static class TicketExtensions
{
    public static TicketSummaryResponse ToDto(this Ticket ticket )
    {
        var commentSummary = ticket.Commenti.Select(c => new CommentoSummaryResponse(c.Autore.Nome, c.Testo, c.Data));
        // Broken implementation. Ticket requires date
        return new TicketSummaryResponse(ticket.Titolo, ticket.Descrizione, DateTime.UtcNow, 
            ticket.Priorità, ticket.Stato, ticket.Utente.Email, commentSummary);
    }
}
