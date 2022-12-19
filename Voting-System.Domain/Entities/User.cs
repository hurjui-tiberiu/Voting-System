using Voting_System.Domain.Enums;

namespace Voting_System.Domain.Entities
{
    public record User
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? IdentityCardId { get; set; }
        public uint? IdentityCardNumber { get; set; }
        public string? IdentityCardSeries { get; set; }
        public DateTime? IdentityCardEmitedDate { get; set; }
        public string? Mail { get; set; }
        public string? Password { get; set; }
        public bool? Voted { get; set; }
        public Role? Role { get; set; }
        public string? Token { get; set; }
    }
}
