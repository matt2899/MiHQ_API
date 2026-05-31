namespace CodePulse.API.Models.DTO
{
    public class RegisterRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
