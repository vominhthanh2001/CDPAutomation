namespace CDPAutomation.Interfaces.Models.Browser
{
    public class Cookie
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? Domain { get; set; }
        public string? Path { get; set; }
        public DateTime? Expiry { get; set; }
    }
}
