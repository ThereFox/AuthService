using AuntificationService.Domain.Entitys;
using Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Tokens> Tokens { get; private set; }
        public DbSet<User> Users { get; private set; }

        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
        }
    }
}
