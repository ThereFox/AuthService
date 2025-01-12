using Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistense.DTOs;

namespace Persistense.Configurations
{
    public class TokensConfiguration : IEntityTypeConfiguration<TokensPairDTO>
    {
        public void Configure(EntityTypeBuilder<TokensPairDTO> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(ex => ex.CreatedAt);

            
            builder.Property(ex => ex.RefreshToken)
                .IsRequired()
                .HasColumnName("RefreshToken");

            builder.Property(ex => ex.AuthToken)
                .IsRequired()
                .HasColumnName("AuthToken");

            builder.HasOne(ex => ex.User)
                .WithMany(ex => ex.Tokens)
                .HasForeignKey(ex => ex.OwnerId)
                .HasPrincipalKey(ex => ex.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
