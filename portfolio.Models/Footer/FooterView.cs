namespace portfolio.Models.Footer
{
    public class FooterView
    {
        public string? Content { get; set; }

        public FooterView(Footer footer, string langCode)
        {
            Content = langCode == "pl" ? footer.ContentPL : footer.ContentENG;
        }
    }
}
