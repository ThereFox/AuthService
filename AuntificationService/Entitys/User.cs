using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuntificationService.Domain.Entitys
{
    public class User : Entity<Guid>
    {
        public UserRole Role { get; private set; }
        public AuthCredentials Credentials { get; init; }
        
        
    }
}
