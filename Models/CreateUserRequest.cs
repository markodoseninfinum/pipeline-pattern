namespace Workshop.Models
{
    public class CreateUserRequest
    {
        public string Name { get; set; } = default!;
        public UserType Type { get; set; }
    }
}
