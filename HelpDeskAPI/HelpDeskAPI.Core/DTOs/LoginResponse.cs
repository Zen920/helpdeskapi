using HelpDeskAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDeskAPI.Core.DTOs;

public record LoginResponse(string Email, int Id, Ruolo Role);
