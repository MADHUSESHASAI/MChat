namespace MChatBackend.DTO
{
    public class AuthenticationResponse
    {
        public Guid Id { get; set; }    
        public string? PersonName {  get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }

        public DateTime ExpirationTime { get; set; }
    }
}
