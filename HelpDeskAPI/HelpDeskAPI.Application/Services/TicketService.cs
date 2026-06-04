using HelpDeskAPI.Application.Interfaces;
using HelpDeskAPI.Core.DTOs;
using HelpDeskAPI.Core.Models;

namespace HelpDeskAPI.Application.Services;

public class TicketService(ITicketRepository _ticketRepo, ICommentoRepository _commentoRepo,
    IAssegnazioneRepository _assegnazioneRepo, IUtenteRepository _utenteRepo) : ITicketService
{
    private readonly ITicketRepository _ticketRepo = _ticketRepo;
    private readonly ICommentoRepository _commentoRepo = _commentoRepo;
    private readonly IAssegnazioneRepository _assegnazioneRepo = _assegnazioneRepo;
    private readonly IUtenteRepository _utenteRepo = _utenteRepo;


    public Task AddCommentToTicketAsync(int ticketId, AddCommentRequest commentDto)
    {
        throw new NotImplementedException();
    }

    public Task AssignTicketToUserAsync(int ticketId, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<int> CreateTicketAsync(CreateTicketRequest ticketDto)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TicketSummaryResponse>> GetActiveTicketsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TicketSummaryResponse?> GetTicketByIdAsync(int ticketId)
    {
        throw new NotImplementedException();
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
