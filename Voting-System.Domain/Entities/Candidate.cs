using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting_System.Domain.Enums;

namespace Voting_System.Domain.Entities
{
    public record Candidate
    {
        public Guid Id { get; set; }
        public string? FullName { set; get; }
        public DateTime DateOfBirth { set; get; }
        public Party PoliticalParty { set; get; }
        public string? ShortDescription { set; get; } 
        public uint? Votes { set; get; } = 0;
    }
}