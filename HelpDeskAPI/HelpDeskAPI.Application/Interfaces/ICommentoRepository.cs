using HelpDeskAPI.Core.DTOs;
using HelpDeskAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDeskAPI.Application.Interfaces;

public interface ICommentoRepository : IRepository<Commento, int>
{
    Task<ICollection<CommentoSummaryResponse>> GetCommentsOfTicket(int tickedId);
}
