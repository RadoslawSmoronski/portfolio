# 🌐 English | 🇵🇱 Polski

# Projekt Portfolio

Dynamiczna strona portfolio stworzona w technologii .NET, z frontendem opartym o Bootstrap.  
Baza danych PostgreSQL jest obsługiwana przez Entity Framework, a część danych przechowywana jest w plikach JSON.

Demo: wkrótce  
Demo panelu administratora: wkrótce  
**Domyślne hasło do panelu admina:** `admin`

---

# Technologie

- **Języki programowania:**
  - C#
  - HTML
  - CSS
  - JavaScript

- **Frameworki:**
  - ASP.NET Core MVC 2.2.5
  - Entity Framework 8.0.6
  - Newtonsoft.Json 13.0.3
  - BCrypt.Net-Next 4.0.3

- **Baza danych:**
  - PostgreSQL 13

- **Pozostałe biblioteki:**
  - SweetAlert2
  - Toastr

---

# Opis działania

Strona zawiera panel administratora dostępny pod adresem `/admin`, zabezpieczony hasłem (domyślnie `admin`).  
W panelu można zarządzać:

- ustawieniami ogólnymi (zakładki, obrazy, tytuły, menu, sekcja powitalna, stopka, zmiana hasła),
- sekcją „O mnie” (nagłówek, zdjęcie, treść),
- sekcją „Umiejętności” (obrazy, treść),
- sekcją „Portfolio” (obrazy, treść, nagłówek, linki),
- sekcją „Kontakt” (podgląd wiadomości z formularza, szablony wiadomości, ustawienia SMTP),
- sekcją „Kontakty” (ikony social media, treść).

---

# Konfiguracja projektu

Konfiguracja odbywa się w `appsettings.json` oraz `ApplicationDbContext` w `portfolio.DataAccess/Data`.  
Po zmianach w modelach danych konieczne jest wykonanie migracji.

---

## appsettings.json

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=;Database=;Port=;Username=;Password=;"
}
```

---

## ApplicationDbContext

W pliku można zmienić domyślne hasło do panelu admina oraz ustawienia SMTP.

```csharp
AdminLogin adminLogin = new AdminLogin
{
    Password = "$2a$11$8WGPCFiXVzavlpu6KaqakO738nLjnUrvioepPN0VwnQ3SD6SZZKUS" // hasło domyślne: admin
};

EmailSettings emailSettings = new EmailSettings
{
    Email = "",
    Password = "",
    SmtpServer = "",
    SmtpPort = 0,
    Encryption = false
};
```

---

# 🚀 Uruchamianie projektu w Dockerze (zalecane)

Projekt posiada pełną konfigurację Docker (API + baza PostgreSQL).  
Całość można uruchomić jedną komendą.

---

## Wymagania

- Docker Desktop (Windows/macOS) lub Docker Engine (Linux)
- Docker Compose v2+

---

## ⚠️ Przed uruchomieniem — skonfiguruj dane bazy danych

Upewnij się, że dane w `docker-compose.yml` są poprawne:

### 1. Ustawienia kontenera PostgreSQL

```yaml
POSTGRES_USER: postgres
POSTGRES_PASSWORD: haslo123
POSTGRES_DB: portfolio
```

### 2. Connection string w kontenerze API

```yaml
ConnectionStrings__DefaultConnection: "Host=db;Port=5432;Database=portfolio;Username=postgres;Password=haslo123"
```

Te wartości **muszą być takie same**.

---

## Domyślne ustawienia:

- Baza danych: `portfolio`
- Użytkownik: `postgres`
- Hasło: `haslo123`

---

## Uruchamianie aplikacji

W katalogu głównym projektu:

```bash
docker compose up --build
```

Docker:

- zbuduje aplikację ASP.NET Core,
- uruchomi PostgreSQL,
- poczeka aż baza będzie gotowa,
- wykona migracje,
- uruchomi API na **http://localhost:5001**

---

## Dostęp do aplikacji

Strona główna:  
http://localhost:5001

Panel administratora:  
http://localhost:5001/admin

**Domyślne hasło admina:**  
admin

---

## Zatrzymywanie kontenerów

```bash
docker compose down
```

Usunięcie danych bazy:

```bash
docker compose down -v
```

---

## Autor

- Radosław Smoroński  
- Kontakt: email@rsmoronski.pl

## Licencja

Projekt udostępniany na licencji MIT. Szczegóły znajdują się w pliku LICENSE.
