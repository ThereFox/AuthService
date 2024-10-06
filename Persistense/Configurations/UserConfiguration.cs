using AuntificationService.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .OwnsOne(ex => ex.Role)
                .Property(ex => ex.RoleId)
                .HasColumnName("RoleId");

            var credentials = builder.OwnsOne(ex => ex.Credentials);

            credentials
                .Property(ex => ex.Login)
                .HasColumnName("Login")
                .IsRequired();

            credentials
                .Property(ex => ex.PasswordHash)
                .HasColumnName("Password")
                .IsRequired();

            builder.HasMany(ex => ex.OwnedTokens)
                .WithOne(ex => ex.Owner)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
