using HelpDeskAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDeskAPI.Core.DTOs;

public record UpdateTicketRequest(int TicketId, Stato NewStatus);
