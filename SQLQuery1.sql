-- 1. Kasutajad
INSERT INTO Kasutajad (Eesnimi, Perekonnanimi, Email, Parool) VALUES
('Mart',  'Tamm', 'mart@tervis.ee',  'parool123'),
('Liisa', 'Mets', 'liisa@tervis.ee', 'parool456');

-- 2. Patsiendid (KasutajaId 1 = Mart, 2 = Liisa)
INSERT INTO Patsiendid (Eesnimi, Perekonnanimi, Isikukood, Synniaeg, KasutajaId) VALUES
('Jaan',   'Kask', '38501010001', '1985-01-01', 1),
('Anna',   'Lepp', '49002025678', '1990-02-02', 1),
('Toomas', 'Saar', '37803153456', '1978-03-15', 2);

-- 3. Toiduained
INSERT INTO Toiduained (Nimetus, Energia, Valgud, Susivesikud, MillestSuhkrud, Rasvad, MillestKullastunud, Kiudained, Sool) VALUES
('Leib', 250, 8.5, 50, 3,  2,   0.5, 6,   1.2),
('Kana', 165, 31,  0,  0,  3.6, 1,   0,   0.1),
(N'Õun', 52,  0.3, 14, 10, 0.2, 0,   2.4, 0);

-- 4. KaaluMootmised (PatsientId 1=Jaan, 2=Anna)
INSERT INTO KaaluMootmised (Kuupaev, Kaal, PatsientId) VALUES
('2025-10-01', 75.5, 1),
('2025-10-08', 75.2, 1),
('2025-10-15', 74.8, 1),
('2025-10-15', 62.3, 2);

-- 5. VeresuhkruMootmised
INSERT INTO VeresuhkruMootmised (Kuupaev, Kellaaeg, Veresuhkur, PatsientId) VALUES
('2025-10-15', '08:00:00', 5.4, 1),
('2025-10-15', '14:30:00', 7.2, 1),
('2025-10-15', '20:00:00', 6.1, 1);

-- 6. VererohuMootmised
INSERT INTO VererohuMootmised (Kuupaev, Kellaaeg, Sustoolne, Diastoolne, Pulss, PatsientId) VALUES
('2025-10-15', '09:00:00', 120, 80, 72, 1),
('2025-10-16', '09:00:00', 125, 82, 75, 1);

-- 7. Soogikorrad (Tyyp: 1=hommik, 2=lõuna, 3=õhtu)
INSERT INTO Soogikorrad (Kuupaev, Tyyp, PatsientId) VALUES
('2025-10-15', 1, 1),
('2025-10-15', 2, 1);

-- 8. SoogikorraRead (SoogikordId 1=hommik, 2=lõuna; ToiduaineId 1=Leib, 2=Kana, 3=Õun)
INSERT INTO SoogikorraRead (Kogus, SoogikordId, ToiduaineId) VALUES
(100, 1, 1),
(50,  1, 3),
(200, 2, 2),
(80,  2, 1);


INSERT INTO ToDoLists (Name) VALUES ('Test list');
INSERT INTO ToDoItems (Title, IsDone, ToDoListId) VALUES ('Test task', 0, 1);