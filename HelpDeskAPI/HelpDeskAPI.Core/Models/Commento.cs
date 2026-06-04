using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HelpDeskAPI.Core.Models;

public class Commento
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int TicketId { get; set;  }
    [Required]
    public int UtenteId { get; set; }
    [MinLength(5)]
    public string Testo { get; set; }
    [Required]
    public DateTime Data { get; set;  }
    public Ticket Ticket { get; set; }
    public Utente Autore { get; set; }
}
