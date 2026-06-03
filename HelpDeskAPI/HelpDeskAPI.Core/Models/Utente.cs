using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace HelpDeskAPI.Core.Models;

public class Utente
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Required, MinLength(5), MaxLength(20)]
    public string Password { get; set; }
    public Ruolo Ruolo { get; set; }

}

public enum Ruolo
{
    USER,
    OPERATOR,
    ADMIN
}