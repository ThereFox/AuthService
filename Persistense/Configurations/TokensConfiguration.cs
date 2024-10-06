using Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.Configurations
{
    public class TokensConfiguration : IEntityTypeConfiguration<Tokens>
    {
        public void Configure(EntityTypeBuilder<Tokens> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(ex => ex.IsDisabled)
                .HasDefaultValue(false);

            builder.Property(ex => ex.CreateDate)
                .HasDefaultValue(DateTime.Now);

            var tokens = builder.OwnsOne(ex => ex.TokensPair);

            tokens.Property(ex => ex.RefreshToken)
                .IsRequired()
                .HasColumnName("RefreshToken");

            tokens.Property(ex => ex.Auth)
                .IsRequired()
                .HasColumnName("AuthToken");

            builder.HasOne(ex => ex.Owner)
                .WithMany(ex => ex.OwnedTokens)
                .OnDelete(DeleteBehavior.Cascade)
        }
    }
}
