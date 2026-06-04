using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HelpDeskAPI.Core.Models;

public class Ticket
{
    [Key]
    public int Id { get; set;  }
    [MinLength(5), MaxLength(30), Required]
    public string Titolo { get; set; }
    [MaxLength(255)]
    public string Descrizione { get; set;  }
    [Required]
    public Stato Stato { get; set; } = Stato.APERTO;
    [Required]

    public DateTime Date { get; set; } = DateTime.UtcNow;
    public Priorità Priorità { get; set; }
    public int StimaEffort { get; set; }
    public int UtenteId { get; set; }
    public Utente Utente { get; set; }
    public ICollection<Commento> Commenti { get; set; } = [];

}

// Rimbalzo ticket -> riapertura

public enum Stato
{
    APERTO,
    IN_LAVORAZIONE,
    RISOLTO,
    CHIUSO
}

public enum Priorità
{
    BASSA,
    MEDIA,
    ALTA
}
