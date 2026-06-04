using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDeskAPI.Core.DTOs;

public record AssignTicketToUserRequest(int TicketId, int UserId);