🌐 [English](README.md) | 🇵🇱 [Polski](README-pl.md)
# Portfolio project

Dynamic portfolio website built using .NET technology, with the frontend powered by Bootstrap. PostgreSQL database is managed through Entity Framework, while some data is stored in JSON files.

Demo: soon
Demo admin panel: soon
Default admin panel password: admin

# Technologies

- **Programming Languages:**
  - C#
  - HTML
  - CSS
  - JavaScript

- **Frameworks:**
  - ASP.NET Core MVC 2.2.5
  - Entity Framework 8.0.6
  - Newtonsoft.Json 13.0.3
  - BCrypt.Net-Next 4.0.3

- **Database:**
  - PostgreSQL 13


- **Other Libraries:**
  - SweetAlert2
  - Toastr

# Dynamic explanation
The website features an admin panel accessible at /admin, requiring a password for access. Within the admin panel, various aspects of the site can be managed, including:

- General settings: Tabs, images, titles, menu, editing content in the welcome section, footer, and changing the access password.

- Editing the "About Me" section: Header, image, and content.

- Editing the "Skills" section: Image and content.

- Editing the "Portfolio" section: Images, content, header, and links.

- Editing the "Contact" section: Preview of messages sent through the form, settings for automatic message content, and email settings.
- Additionally, there's a "Contacts" section for editing icons and content.

# Website configuration
Configuration is done through the appsettings file and ApplicationDbContext in portfolio.DataAccess/Data. After the configuration you have to do migration.


### appsettings

The appsettings.json file is used to configure the database connection.

```json
  "ConnectionStrings": {
    "DefaultConnection": "Host=;Database=;Port=;Username=;Password=;"
  }
```


### portfolio.DataAccess/Data/ApplicationDbContext

In the ApplicationDbContext file, you can change the default admin panel access password and SMTP email settings.

```c#
    AdminLogin adminLogin = new AdminLogin
    {
        Password = "$2a$11$8WGPCFiXVzavlpu6KaqakO738nLjnUrvioepPN0VwnQ3SD6SZZKUS" // (BCRYPT) default password: admin
    };

    EmailSettings emailSettings = new EmailSettings
    {
        Email = "",
        Password = "",
        SmtpServer = "",
        SmtpPort = 0,
        Encryption = false
    };

    modelBuilder.Entity<ConfigureData>().HasData(
        new ConfigureData { Id = 1, JSON = adminLogin.GetJson()},
        new ConfigureData { Id = 2, JSON = emailSettings.GetJson()}
    );
```

# Running the project using Docker (recommended)

The project provides a complete Docker setup, including the ASP.NET Core API and PostgreSQL database.  
You can run the entire environment using a single command.

---

## **Requirements**

- Docker Desktop (Windows/macOS) or Docker Engine (Linux)
- Docker Compose v2+

---

## ⚠️ Before running Docker — configure database credentials

Make sure to update the database settings in:

### 1. docker-compose.yml

```yml
POSTGRES_USER: postgres
POSTGRES_PASSWORD: password123
POSTGRES_DB: portfolio
```

### 2. API connection string (inside docker-compose.yml)

```yml
ConnectionStrings__DefaultConnection: "Host=db;Port=5432;Database=portfolio;Username=postgres;Password=password123"
```

These values must match.

If you change:
- database name
- username
- password

Update both sections.

## Default settings

Database: portfolio
User: postgres
Password: haslo123

---

## **Running the application**

In the root directory of the project (where `docker-compose.yml` is located), run:

```bash
docker compose up --build
```

Docker will automatically:

- build the ASP.NET Core application  
- start the PostgreSQL database  
- wait until the database is ready  
- apply all Entity Framework migrations  
- start the API on port **5001**

---

## **Accessing the application**

Website:  
http://localhost:5001

Admin panel:  
http://localhost:5001/admin

**Default admin password:**  
admin

---

## **Stopping containers**

Stop the environment:

```bash
docker compose down
```

Stop and remove database data:

```bash
docker compose down -v
```


## Author

- Radosław Smoroński
- Contact: email@rsmoronski.pl

## License

This project is licensed under the MIT License. Details can be found in the LICENSE file.
