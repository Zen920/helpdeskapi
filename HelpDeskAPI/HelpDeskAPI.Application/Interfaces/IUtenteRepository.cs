using HelpDeskAPI.Core.DTOs;
using HelpDeskAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDeskAPI.Application.Interfaces;

public interface IUtenteRepository : IRepository<Utente, int>
{
    Task<LoginInfo> GetUtenteByEmail(string email);
}
