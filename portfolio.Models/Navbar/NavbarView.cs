namespace portfolio.Models.Navbar
{
    public class NavbarView
    {
        public string? Header { get; set; }
        public string? ImageUrl { get; set; } = null;

        public NavbarView(Navbar navbar, string langCode)
        {
            ImageUrl = navbar.ImageUrl;
            Header = langCode == "pl" ? navbar.HeaderPL : navbar.HeaderENG;
        }
    }
}
