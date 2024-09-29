using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Domain.ValueObjects;

namespace Domain.Entitys
{
    public class Tokens : Entity<Guid>
    {
        public DateTime CreateDate { get; set; }
        public TokensPair TokensPair { get; set; }
    }
}
