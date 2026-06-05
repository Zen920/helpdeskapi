using HelpDeskAPI.Application.Extensions;
using HelpDeskAPI.Application.Interfaces;
using HelpDeskAPI.Core.DTOs;
using HelpDeskAPI.Core.Models;

namespace HelpDeskAPI.Application.Services;

public class TicketService(ITicketRepository _ticketRepo,
    ITicketReadRepo _ticketReadRepo,
ICommentoRepository _commentoRepo,
    IAssegnazioneRepository _assegnazioneRepo, IUtenteRepository _utenteRepo, IDBContext _dBContext) : ITicketService
{
    private readonly ITicketRepository _ticketRepo = _ticketRepo;
    private readonly ITicketReadRepo _ticketReadRepo = _ticketReadRepo;
    private readonly ICommentoRepository _commentoRepo = _commentoRepo;
    private readonly IAssegnazioneRepository _assegnazioneRepo = _assegnazioneRepo;
    private readonly IUtenteRepository _utenteRepo = _utenteRepo;
    private readonly IDBContext _dbContext = _dBContext;

    public async Task AddCommentToTicketAsync(AddCommentRequest commentDto)
    {
        var ticket = await _ticketRepo.GetById(commentDto.TicketId) ?? throw new Exception("Ticket not found");
        if (ticket.Stato is Stato.CHIUSO) throw new Exception("Ticket is closed");
        if (!(await _utenteRepo.EntityExists(commentDto.UserId))) throw new Exception("User not found");

        var comment = new Commento { Data = commentDto.date, Testo = commentDto.Text, TicketId = commentDto.TicketId, UtenteId = commentDto.UserId};
        _ = _commentoRepo.Create(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AssignTicketToUserAsync(AssignTicketToUserRequest request)
    {
        var a = new Assegnazione {DataAssegnazione = DateTime.UtcNow, TicketId = request.TicketId, UtenteId = request.UserId };
        _ = _assegnazioneRepo.Create(a);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> CreateTicketAsync(CreateTicketRequest ticketDto)
    {
        var ticket = ticketDto.ToEntity();
        var entity = await _ticketRepo.Create(ticket);
        await _dbContext.SaveChangesAsync();

        return ticket.Id;
    }

    public async Task<ICollection<TicketByStatusResponse>> GetActiveTicketsAsync()
    {
        var list = await _ticketReadRepo.GetActiveTickets();
        return list;
    }

    public async Task<ICollection<CommentoSummaryResponse>> GetCommentsOfTicket(int tickedId)
    {
       var comments = await _commentoRepo.GetCommentsOfTicket(tickedId);
       return comments;
    }

    public async Task<TicketSummaryResponse?> GetTicketByIdAsync(int ticketId)
    {
        var t = await _ticketRepo.GetById(ticketId);
        
        if (t is null) throw new Exception("Ticket not found");
        var response = t.ToDto();
        return response;
    }

    public async Task ReopenTicketAsync(int ticketId)
    {
        var t = await _ticketRepo.GetById(ticketId);
        if (t is null) throw new Exception("Ticket not found");
        if (t.Stato != Stato.CHIUSO) throw new Exception("Ticket is not closed");
        t.Stato = Stato.IN_LAVORAZIONE;
        await _ticketRepo.Update(t);
        await _dbContext.SaveChangesAsync();

    }

    public async Task UpdateTicketStatusAsync(UpdateTicketRequest request)
    {
        var t = await _ticketRepo.GetById(request.TicketId);
        if (t is null) throw new Exception("Ticket not found");
        if (t.Stato == Stato.CHIUSO) throw new Exception("Ticket is closed");
        t.Stato = request.NewStatus;
        await _ticketRepo.Update(t);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsUserAssignedToTicket(int userId, int ticketId)
    {
        if (await _ticketRepo.GetById(ticketId) is null) throw new Exception("Ticket not found");
        if (await _utenteRepo.GetById(userId) is null) throw new Exception("Ticket not found");
        return await _assegnazioneRepo.IsUserAssignedToTicket(userId, ticketId);

    }
}
