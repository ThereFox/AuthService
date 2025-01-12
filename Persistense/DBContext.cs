using AuntificationService.Domain.Entitys;
using Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistense.DTOs;

namespace Persistense
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<TokensPairDTO> Tokens { get; private set; }
        public DbSet<UserDTO> Users { get; private set; }

        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
           var schemeInitialiseSucsess = Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
        }
    }
}
