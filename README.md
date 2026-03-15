# GameLibrary

GameLibrary este un sistem backend robust (API RESTful) dezvoltat pentru gestionarea unei biblioteci cuprinzătoare de jocuri video. Aplicația permite administrarea informațiilor despre jocuri, dezvoltatori, editori (publishers), platforme și genuri, oferind totodată integrare cu un serviciu extern pentru obținerea de jocuri gratuite.

## Funcționalități Principale
* **Gestiunea Jocurilor:** Adăugarea, actualizarea, ștergerea și vizualizarea detaliilor despre jocuri.
* **Catalogare Extinsă:** Organizarea jocurilor în funcție de Gen (Genre), Platformă, Dezvoltator (Developer) și Editor (Publisher).
* **Integrare API Extern:** Conectare la API-ul `FreeToGame` pentru preluarea datelor despre jocuri gratuite.
* **Gestionarea Utilizatorilor:** Crearea de conturi și securizarea parolelor prin hashing (`PasswordHasher`).
* **Sistem avansat de Logging și Error Handling:** Tratarea globală a excepțiilor și logare personalizată de tip Singleton.

## Tehnologii Folosite
* **Framework:** ASP.NET Core (C#)
* **Arhitectură:** Multistratificată (N-Tier Architecture)
* **ORM:** Entity Framework Core
* **Design Patterns:** Repository Pattern, Singleton (pentru Logger), Dependency Injection
* **Testare:** Teste Unitare integrate pentru API, Servicii, Repository și Domain

## Structura Arhitecturală (Soluția)
Proiectul folosește o arhitectură curată, strict modularizată, pentru a separa responsabilitățile:
* **GameLibrary.API**: Punctul de intrare; conține controllerele și definește endpoint-urile.
* **GameLibrary.Domain**: Conține regulile de business (Domain logic) și utilitare de securitate (ex. hashing).
* **GameLibrary.Entity**: Definește structura bazei de date (entitățile: `User`, `Game`, `Developer`, `Platform`, etc.) având la bază un `BaseEntity`.
* **GameLibrary.Repository**: Gestionează contextul bazei de date (`GameLibraryDbContext`) și operațiunile CRUD via Interfețe Repository.
* **GameLibrary.Service**: Conține DTO-urile (Data Transfer Objects), clasele de mapare (Mapping) și aplică logica de servicii între Controller și Repository.
* **GameLibrary.Integration**: Gestionează configurările generale (`AppConfig`), `AppLogger`-ul și un Handler global pentru excepții.
* **GameLibrary.Test**: Proiect dedicat testării unitare și de integrare pentru toate layerele menționate anterior.

## Baza de Date (Entități Principale)
* `Users` - Informații despre utilizatorii platformei.
* `Games` - Informații detaliate despre jocuri.
* `Developers` - Studiourile care au dezvoltat jocurile.
* `Publishers` - Companiile care au publicat jocurile.
* `Platforms` - Platformele pe care rulează (ex. PC, Consolă, Mobile).
* `Genres` - Genul jocurilor (ex. RPG, Action, Strategy).

## API Endpoints
API-ul este separat pe controllere specifice:
* `/api/Games` - Gestiunea jocurilor.
* `/api/Developers` & `/api/Publishers` - Administrarea creatorilor de jocuri.
* `/api/Platforms` & `/api/Genres` - Categorisirea jocurilor.
* `/api/FreeToGame` - Endpoint-uri dedicate interacțiunii cu platforma externă de jocuri gratis.
* `/api/Users` - Administrarea conturilor de utilizatori.

## Cum se rulează proiectul

### Precondiții
* [.NET SDK](https://dotnet.microsoft.com/download) instalat.
* Bază de date SQL Server (sau similar) configurată.
* IDE (Visual Studio / Visual Studio Code / Rider).

### Setup & Rulare
1. Clonează repository-ul.
2. Navighează în folderul `GameLibrary.API`.
3. Configurează fișierul `appsettings.json` adăugând string-ul de conexiune la baza ta de date locală (în secțiunea `ConnectionStrings`).
4. Deschide un terminal și rulează migrările pentru a genera baza de date:
   ```bash
   dotnet ef database update --project ../GameLibrary.Repository
5. Pornește aplicația: dotnet run
6. Odată pornit, poți accesa interfața Swagger (de regulă la https://localhost:<port>/swagger) pentru a testa interactiv toate endpoint-urile.

### Rularea Testelor
Pentru a rula testele unitare care asigură funcționarea corectă a logicii: dotnet test ../GameLibrary.Test

# Team:
### Petre Flaviu-Mihai (10LF332)
### Opriș Liviu-Vlad (10LF332)
### Slav Gabriel Bogdan (10LF333) (GabyTheNephew)
### Vișoiu Radu (10LF333)
