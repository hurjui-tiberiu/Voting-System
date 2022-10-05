namespace Voting_System.Application.Models.UserDto
{
    public class UserPatchDto
    {
        public string? FullName { get; set; }
        public string? IdentityCardId { get; set; }
        public uint? IdentityCardNumber { get; set; }
        public string? IdentityCardSeries { get; set; }
        public DateTime? IdentityCardEmitedDate { get; set; }
        public string? Mail { get; set; }
    }
}
