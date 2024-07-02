namespace Workshop.Models
{
    public class User
    {
        public string Name { get; set; } = default!;
        public UserType Type { get; set; }
        public List<CurrencyAccount> CurrencyAccounts { get; set; } = default!;
    }
}
