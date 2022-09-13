using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting_System.Domain.Entities;
using Voting_System.Infrastructure.Contexts;
using Voting_System.Infrastructure.Interfaces;

namespace Voting_System.Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly EFContext context;

        public CandidateRepository(EFContext context)
        {
            this.context = context;
        }

        public async Task AddCandidateAsync(Candidate candidate)
        {
           context.Add(candidate);

           await context.SaveChangesAsync();
        }

        public async Task<List<Candidate>> GetAllCandidatesAsync()
        {
            var candidates = await context.Candidates.ToListAsync();

            return candidates;
        }

        public async Task RemoveCandidateAsync(Candidate candidate)
        {
            context.Remove(candidate);
            await context.SaveChangesAsync();
        }

        public async Task<Candidate?> GetCandidateAsync (Guid candidateId)
        {
            return await context.Candidates.FirstOrDefaultAsync(entity => entity.Id.Equals(candidateId));
        }

        public async Task UpdateCandidateAsync(Candidate candidate)
        {
            context.Update(candidate);


            await context.SaveChangesAsync();

        }
    }
}
