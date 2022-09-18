using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voting_System.Domain.Entities;

namespace Voting_System.Domain.Configurations
{
    public class CandidateConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName)
                .IsRequired();

            builder.Property(x => x.IdentityCardId)
                .IsRequired();

            builder.Property(x => x.IdentityCardNumber)
               .IsRequired();

            builder.Property(x => x.IdentityCardSeries)
               .IsRequired();

            builder.Property(x => x.Mail)
               .IsRequired();

            builder.Property(x => x.Password)
               .IsRequired();

            builder.Property(x => x.IdentityCardEmitedDate)
               .IsRequired();
        }
    }
}
