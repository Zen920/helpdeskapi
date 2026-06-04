using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HelpDeskAPI.Core.Models;

public class Assegnazione
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int TicketId { get; set; }
    [Required]
    public int UtenteId { get; set;  }
    [Required]
    public DateTime DataAssegnazione { get; set;  }
    public Utente Utente { get; set; }
    public Ticket Ticket { get; set;  }
}
