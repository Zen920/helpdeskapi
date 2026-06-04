using HelpDeskAPI.Application.Interfaces;
using HelpDeskAPI.Application.Services;
using HelpDeskAPI.Core.DTOs;
using HelpDeskAPI.Core.Models;
using NSubstitute;

namespace HelpDeskAPI.Tests.Services;

public class TicketServiceTests
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ICommentoRepository _commentoRepository;
    private readonly IAssegnazioneRepository _assegnazioneRepository;
    private readonly IUtenteRepository _utenteRepository;
    private readonly IDBContext _dbContext;

    private readonly ITicketService _sut;

    public TicketServiceTests()
    {
        _ticketRepository = Substitute.For<ITicketRepository>();
        _commentoRepository = Substitute.For<ICommentoRepository>();
        _assegnazioneRepository = Substitute.For<IAssegnazioneRepository>();
        _utenteRepository = Substitute.For<IUtenteRepository>();
        _dbContext = Substitute.For<IDBContext>();

        _sut = new TicketService(_ticketRepository, _commentoRepository, _assegnazioneRepository, _utenteRepository, _dbContext);
    }

    [Fact]
    public async Task CreateTicketAsync_ValidRequest_ReturnsTicketId()
    {
        var request = new CreateTicketRequest(
            UtenteId: 1,
            Titolo: "Problema connessione",
            Descrizione: "La rete non funziona",
            Priority: Priorità.ALTA,
            Date: DateTime.UtcNow
        );

        Ticket capturedTicket = null;
        _ticketRepository.When(r => r.Create(Arg.Any<Ticket>()))
            .Do(callInfo =>
            {
                capturedTicket = callInfo.Arg<Ticket>();
                capturedTicket.Id = 42;
            });

        var result = await _sut.CreateTicketAsync(request);

        Assert.Equal(42, result);
    }

    [Fact]
    public async Task GetTicketByIdAsync_ExistingTicket_ReturnsTicketSummaryResponse()
    {
        var ticketId = 1;
        var utente = new Utente { Id = 1, Nome = "Mario Rossi", Email = "mario@test.it", Ruolo = Ruolo.OPERATOR };
        var ticket = new Ticket
        {
            Id = ticketId,
            Titolo = "Problema connessione",
            Descrizione = "Rete instabile al piano terra",
            Stato = Stato.IN_LAVORAZIONE,
            Priorità = Priorità.ALTA,
            UtenteId = utente.Id,
            Utente = utente
        };
        var commenti = new List<Commento>
        {
            new()
            {
                Id = 1, TicketId = ticketId, UtenteId = utente.Id,
                Testo = "Stiamo verificando", Data = DateTime.UtcNow,
                Autore = utente
            }
        };
        var assegnazioni = new List<Assegnazione>
        {
            new()
            {
                Id = 1, TicketId = ticketId, UtenteId = utente.Id,
                DataAssegnazione = DateTime.UtcNow, Utente = utente
            }
        };

        _ticketRepository.GetById(ticketId).Returns(ticket);
        _commentoRepository.GetAll().Returns(commenti);
        _assegnazioneRepository.GetAll().Returns(assegnazioni);
        _utenteRepository.GetById(utente.Id).Returns(utente);

        var result = await _sut.GetTicketByIdAsync(ticketId);

        Assert.NotNull(result);
        Assert.Equal(ticketId, ticket.Id);
    }

    [Fact]
    public async Task GetTicketByIdAsync_NonExistingTicket_ReturnsNull()
    {
        _ticketRepository.GetById(Arg.Any<int>()).Returns((Ticket?)null);

        var result = await _sut.GetTicketByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public void GetActiveTicketsAsync_WithActiveTickets_ReturnsOnlyActiveOnes() { Assert.Equal(0,1); }

    [Fact]
    public void GetActiveTicketsAsync_NoActiveTickets_ReturnsEmptyCollection() { Assert.Equal(0, 1); }

    [Fact]
    public void UpdateTicketStatusAsync_ValidTransition_UpdatesSuccessfully() { Assert.Equal(0, 1); }

    [Fact]
    public void UpdateTicketStatusAsync_InvalidTransition_ThrowsInvalidOperationException() { Assert.Equal(0, 1); }

    [Fact]
    public async Task UpdateTicketStatusAsync_NonExistingTicket_ThrowsKeyNotFoundException() {
        _ticketRepository.GetById(Arg.Any<int>()).Returns((Ticket?)null);
        await Assert.ThrowsAsync<Exception>(async () =>
        {
           await _sut.UpdateTicketStatusAsync(999, Stato.CHIUSO);
        }
        );
    }

    [Fact]
    public void AddCommentToTicketAsync_ValidRequest_AddsComment() {
        var commento = new AddCommentRequest("testo commento", DateTime.UtcNow, 1, 1);
        _sut.AddCommentToTicketAsync(commento);
    }

    [Fact]
    public async Task AddCommentToTicketAsync_NonExistingTicket_ThrowsKeyNotFoundException() {
        var commento = new AddCommentRequest("testo commento", DateTime.UtcNow, 1, 10);
        _ = _ticketRepository.GetById(commento.TicketId).Returns((Ticket?)null);
        await Assert.ThrowsAsync<Exception>(async () =>
        {
            await _sut.AddCommentToTicketAsync( commento);
        });
    }

    [Fact]
    public async Task AddCommentToTicketAsync_ClosedTicket_ThrowsInvalidOperationException() {
        var commento = new AddCommentRequest("testo commento", DateTime.UtcNow, 1, 10);
        var ticket = new Ticket { Id = commento.TicketId, Stato = Stato.CHIUSO};
        _ticketRepository.GetById(commento.TicketId).Returns(ticket);
        await Assert.ThrowsAsync<Exception>(async () =>
        {
            await _sut.AddCommentToTicketAsync(commento);
        });
    }

    [Fact]
    public void AssignTicketToUserAsync_ValidRequest_CreatesAssegnazione() { Assert.Equal(0, 1); }

    [Fact]
    public void AssignTicketToUserAsync_NonExistingTicket_ThrowsKeyNotFoundException() { Assert.Equal(0, 1); }

    [Fact]
    public void AssignTicketToUserAsync_NonExistingUser_ThrowsKeyNotFoundException() { Assert.Equal(0, 1); }

    [Fact]
    public void AssignTicketToUserAsync_TicketAlreadyAssigned_ThrowsInvalidOperationException() { Assert.Equal(0, 1); }

    [Fact]
    public void ReopenTicketAsync_ClosedTicket_SetsStatusToAperto() { Assert.Equal(0, 1); }

    [Fact]
    public void ReopenTicketAsync_NonExistingTicket_ThrowsKeyNotFoundException() { Assert.Equal(0, 1); }

    [Fact]
    public void ReopenTicketAsync_TicketAlreadyOpen_ThrowsInvalidOperationException() { Assert.Equal(0, 1); }
}
