using HelpDeskAPI.Application.Extensions;
using HelpDeskAPI.Application.Interfaces;
using HelpDeskAPI.Core.DTOs;
using HelpDeskAPI.Core.Models;

namespace HelpDeskAPI.Application.Services;

public class TicketService(ITicketRepository _ticketRepo, ICommentoRepository _commentoRepo,
    IAssegnazioneRepository _assegnazioneRepo, IUtenteRepository _utenteRepo, IDBContext _dBContext) : ITicketService
{
    private readonly ITicketRepository _ticketRepo = _ticketRepo;
    private readonly ICommentoRepository _commentoRepo = _commentoRepo;
    private readonly IAssegnazioneRepository _assegnazioneRepo = _assegnazioneRepo;
    private readonly IUtenteRepository _utenteRepo = _utenteRepo;
    private readonly IDBContext _dbContext = _dBContext;

    public async Task AddCommentToTicketAsync(AddCommentRequest commentDto)
    {
        var ticket = await _ticketRepo.GetById(commentDto.TicketId) ?? throw new Exception("Ticket not found");
        if (ticket.Stato is Stato.CHIUSO) throw new Exception("Ticket is closed");
        if (!(await _utenteRepo.EntityExists(commentDto.TicketId))) throw new Exception("User not found");

        var comment = new Commento { Data = commentDto.date, Testo = commentDto.Text, TicketId = commentDto.TicketId, UtenteId = commentDto.UserId};
        _ = _commentoRepo.Create(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AssignTicketToUserAsync(int ticketId, int userId)
    {
        var a = new Assegnazione {DataAssegnazione = DateTime.UtcNow, TicketId = ticketId, UtenteId = userId };
        _ = _assegnazioneRepo.Create(a);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> CreateTicketAsync(CreateTicketRequest ticketDto)
    {
        var entity = await _ticketRepo.Create(ticketDto.ToEntity());
        return entity;
    }

    public Task<IEnumerable<TicketSummaryResponse>> GetActiveTicketsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<TicketSummaryResponse?> GetTicketByIdAsync(int ticketId)
    {
        var t = await _ticketRepo.GetById(ticketId);
        
        if (t is null) throw new Exception("Ticket not found");
        var response = t.ToDto();
        return response;
    }

    public Task ReopenTicketAsync(int ticketId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTicketStatusAsync(int ticketId, Stato newStatus)
    {
        throw new NotImplementedException();
    }
}
