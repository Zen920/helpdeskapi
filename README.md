# helpdeskapi

HelpDeskAPI è il progetto Capstone realizzato nell'ambito dell'Azure Academy. Si tratta di una soluzione backend robusta e scalabile per la gestione del ciclo di vita dei ticket di assistenza tecnica (Help Desk), progettata seguendo le migliori pratiche dello sviluppo software enterprise.

# Caratteristiche principali

- Clean Architecture: Separazione netta delle responsabilità per garantire testabilità, modularità e indipendenza dai framework esterni.
- Sviluppo CodeFirst: Database relazionale locale basato su SQL Server, generato direttamente a partire dal modello a oggetti tramite Entity Framework Core.
- Approccio TDD (Test-Driven Development): Sviluppo guidato dai test per le funzionalità core del sistema, assicurando la massima stabilità logica.
- Integrazione gRPC: Comunicazione asincrona e ad alte prestazioni dedicata al logging automatico dei cambi di stato dei ticket.
- Sicurezza Avanzata: Autenticazione e autorizzazione stateless implementata tramite JSON Web Token (JWT) e policy di accesso di ASP.NET Core.

# Funzionalità (in sviluppo)

Il sistema risponde ai seguenti requisiti funzionali e flussi di business:

- Registrazione e Login Utente: Gestione sicura degli accessi e rilascio di token JWT firmati contenenti i ruoli associati (Utente, Operatore, Admin).

- Creazione Ticket: Gli utenti autenticati possono aprire una richiesta di supporto inserendo un titolo, una descrizione e i dettagli correlati.

- Assegnazione dei Ticket: Smistamento controllato dei ticket aperti esclusivamente alle figure con ruolo Operatore.

- Gestione del Ciclo di Vita (Workflow): Avanzamento dello stato del ticket (es. Aperto, In Lavorazione, Chiuso). Ad ogni cambio di stato, viene scatenato un evento di log ad alte prestazioni via gRPC.

- Sistema di Commenti: Possibilità di inserire note e risposte all'interno dei ticket, con accesso ristretto esclusivamente agli operatori assegnati e agli utenti amministratori (Admin).
