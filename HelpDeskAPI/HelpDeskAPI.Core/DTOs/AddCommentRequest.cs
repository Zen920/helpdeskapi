using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDeskAPI.Core.DTOs;

public record AddCommentRequest(string Text, DateTime date, int TicketId, int UserId);