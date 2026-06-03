using HelpDeskAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDeskAPI.Application.Interfaces;

public interface IAuthService
{
    Task<Utente> Login(string email, string password);
}
