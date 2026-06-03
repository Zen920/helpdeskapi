using HelpDeskAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDeskAPI.Core.DTOs;

public record TicketSummaryResponse(string Titolo, string Descrizione, DateTime Data,
    Priorità Priority, Stato Status, string? UserWorkingOnIt, IEnumerable<CommentoSummaryResponse> Comments);
