using AuntificationService.Domain.Entitys;
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
    public class UserConfiguration : IEntityTypeConfiguration<UserDTO>
    {
        public void Configure(EntityTypeBuilder<UserDTO> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("Id");
            
            builder
                .Property(ex => ex.RoleId)
                .HasColumnName("RoleId");

            builder
                .Property(ex => ex.Login)
                .HasColumnName("Login")
                .IsRequired();

            builder
                .Property(ex => ex.Password)
                .HasColumnName("Password")
                .IsRequired();

            builder.HasMany(ex => ex.Tokens)
                .WithOne(ex => ex.User)
                .IsRequired()
                .HasForeignKey(ex => ex.OwnerId)
                .HasPrincipalKey(ex => ex.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
