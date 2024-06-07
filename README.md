# Portfolio project

Dynamic portfolio website built using .NET technology, with the frontend powered by Bootstrap. PostgreSQL database is managed through Entity Framework, while some data is stored in JSON files.

Demo: https://rsmoronski.azurewebsites.net/

# Dynamic explanation
The website features an admin panel accessible at /admin, requiring a password for access. Within the admin panel, various aspects of the site can be managed, including:

- General settings: Tabs, images, titles, menu, editing content in the welcome section, footer, and changing the access password.

- Editing the "About Me" section: Header, image, and content.

- Editing the "Skills" section: Image and content.

- Editing the "Portfolio" section: Images, content, header, and links.

- Editing the "Contact" section: Preview of messages sent through the form, settings for automatic message content, and email settings.
- Additionally, there's a "Contacts" section for editing icons and content.

# Website configuration
Configuration is done through the appsettings file and ApplicationDbContext in portfolio.DataAccess/Data.


### appsettings

The appsettings.json file is used to configure the database connection.

```json
  "ConnectionStrings": {
    "DefaultConnection": "Host=host;Database=database;Port=port;Username=username;Password=password"
  }
```


### portfolio.DataAccess/Data/ApplicationDbContext

In the ApplicationDbContext file, you can change the default admin panel access password and SMTP email settings.

```c#
AdminLogin adminLogin = new AdminLogin
{
    Password = "$2a$11$8WGPCFiXVzavlpu6KaqakO738nLjnUrvioepPN0VwnQ3SD6SZZKUS"
};

EmailSettings emailSettings = new EmailSettings
{
    Email = "email",
    Password = "password",
    SmtpServer = "smtp server"",
    SmtpPort = 000,
    Encryption = false
};

modelBuilder.Entity<ConfigureData>().HasData(
    new ConfigureData { Id = 1, JSON = adminLogin.GetJson()},
    new ConfigureData { Id = 2, JSON = emailSettings.GetJson()}
);
```

## Author

- Radosław Smoroński
- Contact: radoslaw.smoronski@gmail.com

## License

This project is licensed under the MIT License. Details can be found in the LICENSE file.
