using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting_System.Application.Models.CandidateDto
{
    public class CandidatesVotingStatus
    {
        public string? Name { get; set; }
        public uint? Votes { get; set; }
        public decimal? PercentageOfVotes { get; set; }
    }
}
