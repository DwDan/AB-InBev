using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.Common
{
    public class UserApplication
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public NameApplication Name { get; set; } = new();

        public AddressApplication Address { get; set; } = new();

        public string Phone { get; set; } = string.Empty;

        public UserStatus Status { get; set; }

        public UserRole Role { get; set; }
    }
}
