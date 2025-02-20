namespace Nate.Data.EntityFrameworkCore.Sample.Models.Dtos.Requests
{
    public class CreateUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email2 { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string Province { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
    }
}
