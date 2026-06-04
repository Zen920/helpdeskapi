-- ============================================================
-- HELP DESK API - SEED DATA
-- Target: SQL Server (LocalDB)
-- Database: HelpDeskAPIDB
-- ============================================================

-- Disable FK checks for clean inserts
SET IDENTITY_INSERT [dbo].[Utenti] ON;

-- ============================================================
-- UTENTI (Users) — 8 rows
-- Ruolo: 0=USER, 1=OPERATOR, 2=ADMIN
-- ============================================================
INSERT INTO [dbo].[Utenti] ([Id], [Nome], [Email], [Password], [Ruolo])
VALUES
    (1, N'Mario Rossi',    N'mario.rossi@azienda.it',    N'Passw0rd!',   2),
    (2, N'Laura Bianchi',  N'laura.bianchi@azienda.it',  N'Passw0rd!',   2),
    (3, N'Giuseppe Verdi', N'giuseppe.verdi@azienda.it', N'Passw0rd!',   1),
    (4, N'Anna Neri',      N'anna.neri@azienda.it',      N'Passw0rd!',   1),
    (5, N'Marco Gialli',   N'marco.gialli@azienda.it',   N'Passw0rd!',   1),
    (6, N'Sofia Blu',      N'sofia.blu@azienda.it',      N'Passw0rd!',   0),
    (7, N'Luca Marroni',   N'luca.marroni@azienda.it',   N'Passw0rd!',   0),
    (8, N'Chiara Viola',   N'chiara.viola@azienda.it',   N'Passw0rd!',   0);

SET IDENTITY_INSERT [dbo].[Utenti] OFF;

SET IDENTITY_INSERT [dbo].[Tickets] ON;

-- ============================================================
-- TICKETS — 15 rows
-- Stato: 0=APERTO, 1=IN_LAVORAZIONE, 2=RISOLTO, 3=CHIUSO
-- Priorità: 0=BASSA, 1=MEDIA, 2=ALTA
-- ============================================================
INSERT INTO [dbo].[Tickets] ([Id], [Titolo], [Descrizione], [Stato], [Priorità], [StimaEffort], [Date], [UtenteId])
VALUES
    (1,  N'Stampante non funziona',        N'La stampante del piano terra non risponde ai comandi di stampa.',                 0, 2, 4,  '2026-06-01 09:15:00', 6),
    (2,  N'Errore login CRM',              N'Impossibile accedere al CRM con le proprie credenziali.',                        1, 1, 3,  '2026-06-01 10:30:00', 7),
    (3,  N'Richiesta nuovo PC',            N'Nuovo sviluppatore assunto, necessita di una postazione completa.',              0, 0, 8,  '2026-06-01 14:00:00', 6),
    (4,  N'VPN non funzionante da remoto', N'Dal collegamento VPN il client restituisce errore 800.',                         1, 2, 5,  '2026-06-02 08:45:00', 8),
    (5,  N'Casella email piena',           N'La casella di posta ha superato la quota consentita.',                           2, 0, 1,  '2026-06-02 11:20:00', 7),
    (6,  N'Installazione software CAD',    N'Serve licenza e installazione di AutoCAD per ufficio tecnico.',                 0, 1, 6,  '2026-06-03 09:00:00', 6),
    (7,  N'Monitoraggio server caduto',    N'Il servizio di monitoraggio Zabbix non risponde da ieri sera.',                  1, 2, 8,  '2026-06-03 10:30:00', 8),
    (8,  N'Reset password Outlook',        N'Utente ha dimenticato la password di Outlook e non riceve i codici di reset.',   3, 1, 1,  '2026-06-03 11:00:00', 7),
    (9,  N'WiFi ospiti non funziona',      N'La rete WiFi dedicata agli ospiti non si connette.',                             0, 1, 3,  '2026-06-04 07:30:00', 6),
    (10, N'Backup database fallito',       N'Il job notturno di backup del DB produzione non è andato a buon fine.',          1, 2, 7,  '2026-06-04 08:00:00', 8),
    (11, N'Richiesta badge sostitutivo',   N'Badge smarrito, richiesto nuovo rilascio con credenziali aggiornate.',           2, 0, 2,  '2026-06-04 09:30:00', 7),
    (12, N'Problema condivisione cartella',N'La cartella condivisa del reparto HR non è accessibile da alcuni utenti.',       0, 1, 4,  '2026-06-04 10:00:00', 6),
    (13, N'Aggiornamento sistema ERP',     N'Rilasciare l''ultimo aggiornamento del modulo contabilità sull''ERP.',           0, 2, 10, '2026-06-04 11:15:00', 8),
    (14, N'Scanner fuori rete',            N'Lo scanner del reparto spedizioni non viene rilevato in rete.',                  1, 1, 3,  '2026-06-04 13:00:00', 7),
    (15, N'Migrazione account Teams',      N'Allineare l''account Teams del nuovo utente con la rubrica globale.',            3, 0, 2,  '2026-06-04 14:30:00', 6);

SET IDENTITY_INSERT [dbo].[Tickets] OFF;

SET IDENTITY_INSERT [dbo].[Commenti] ON;

-- ============================================================
-- COMMENTI — 18 rows
-- ============================================================
INSERT INTO [dbo].[Commenti] ([Id], [TicketId], [UtenteId], [Testo], [Data])
VALUES
    -- Ticket 1: Stampante
    (1,  1, 3, N'Ho verificato la coda di stampa, nessun blocco apparente. Procedo con il riavvio del servizio.',              '2026-06-01 09:45:00'),
    (2,  1, 3, N'Riavvio completato, test in corso.',                                                                         '2026-06-01 10:00:00'),
    (3,  1, 6, N'La stampante ancora non stampa. Provato anche da un altro PC.',                                               '2026-06-01 10:30:00'),

    -- Ticket 2: Login CRM
    (4,  2, 4, N'Reset della password eseguito. Invio credenziali temporanee.',                                                '2026-06-01 11:00:00'),
    (5,  2, 7, N'Funziona! Grazie mille.',                                                                                    '2026-06-01 11:15:00'),

    -- Ticket 4: VPN
    (6,  4, 5, N'Errore 800 indica problema di protocollo. Verifico le policy del firewall.',                                 '2026-06-02 09:15:00'),
    (7,  4, 5, N'Aggiornata regola VPN sul firewall. Test in corso.',                                                         '2026-06-02 10:00:00'),

    -- Ticket 5: Email piena
    (8,  5, 3, N'Archiviati i messaggi più vecchi di 6 mesi. Quota ripristinata.',                                             '2026-06-02 11:45:00'),
    (9,  5, 7, N'Confermo, ora funziona tutto.',                                                                              '2026-06-02 12:00:00'),

    -- Ticket 7: Monitoraggio server
    (10, 7, 4, N'Riavvio del servizio Zabbix. Verifico i log di sistema.',                                                    '2026-06-03 11:15:00'),
    (11, 7, 4, N'Trovato errore di configurazione nel file zabbix_server.conf. Correzione applicata.',                         '2026-06-03 11:45:00'),

    -- Ticket 8: Password Outlook
    (12, 8, 3, N'Password resettata tramite pannello amministratore 365. Utente informato via SMS.',                           '2026-06-03 11:30:00'),
    (13, 8, 7, N'Confermo, ora riesco ad accedere. Grazie.',                                                                  '2026-06-03 12:00:00'),

    -- Ticket 10: Backup fallito
    (14, 10, 5, N'Il disco di backup era pieno. Avviata pulizia e nuovo backup manuale.',                                      '2026-06-04 08:45:00'),
    (15, 10, 5, N'Backup completato con successo. Programmata verifica settimanale dello spazio disco.',                        '2026-06-04 09:30:00'),

    -- Ticket 14: Scanner fuori rete
    (16, 14, 4, N'Verificata assegnazione IP: conflitto con un altro dispositivo. Assegnata IP statico.',                      '2026-06-04 13:45:00'),
    (17, 14, 4, N'Scanner raggiungibile. Test di scansione riuscito.',                                                         '2026-06-04 14:00:00'),
    (18, 14, 7, N'Confermo, lo scanner funziona correttamente.',                                                               '2026-06-04 14:15:00');

SET IDENTITY_INSERT [dbo].[Commenti] OFF;

SET IDENTITY_INSERT [dbo].[Assegnazioni] ON;

-- ============================================================
-- ASSEGNAZIONI — 10 rows
-- ============================================================
INSERT INTO [dbo].[Assegnazioni] ([Id], [TicketId], [UtenteId], [DataAssegnazione])
VALUES
    (1,  1,  3, '2026-06-01 09:30:00'),
    (2,  2,  4, '2026-06-01 10:45:00'),
    (3,  4,  5, '2026-06-02 09:00:00'),
    (4,  5,  3, '2026-06-02 11:30:00'),
    (5,  7,  4, '2026-06-03 10:45:00'),
    (6,  8,  3, '2026-06-03 11:15:00'),
    (7,  10, 5, '2026-06-04 08:15:00'),
    (8,  13, 4, '2026-06-04 11:30:00'),
    (9,  14, 4, '2026-06-04 13:15:00'),
    (10, 15, 3, '2026-06-04 14:45:00');

SET IDENTITY_INSERT [dbo].[Assegnazioni] OFF;
