namespace portfolio.Models.ViewModels
{
    public class AdminGeneralViewModel
    {
        public WebsiteTab.WebsiteTab? WebsiteTab { get; set; }
        public Navbar.Navbar? Navbar { get; set; }
        public Welcome.Welcome? Welcome { get; set; }
        public Footer.Footer? Footer { get; set; }
        public EditAdminLogin? EditAdminLogin { get; set; }
    }
}
