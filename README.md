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
The site configuration is done through the appsettings file, where parameters such as allowed hosts, database connection, email settings, and admin login data can be set.

This project enables easy management of website content and configuration, making it an ideal tool for individuals looking to showcase their skills and projects professionally.

```json
  "ConnectionStrings": {
    "DefaultConnection": "Host=host;Database=database;Port=port;Username=username;Password=password"
  },
  "EmailSettings": {
    "Email": "email",
    "Password": "password",
    "SmtpServer": "smtpServer",
    "SmtpPort": "smtpPort",
    "Encryption": "EncryptionBool"
  },
  "AdminLogin": {
    "Password": "$2a$11$8WGPCFiXVzavlpu6KaqakO738nLjnUrvioepPN0VwnQ3SD6SZZKUS" //default password - admin
  }
```

## Author

- Radosław Smoroński
- Contact: radoslaw.smoronski@gmail.com

## License

This project is licensed under the MIT License. Details can be found in the LICENSE file.
