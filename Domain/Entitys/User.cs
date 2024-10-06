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
        private readonly List<Tokens> _tokens;

        public UserRole Role { get; private set; }
        public AuthCredentials Credentials { get; init; }

        public IReadOnlyCollection<Tokens> OwnedTokens => _tokens;

        private User(Guid id, UserRole role, AuthCredentials credentials, List<Tokens> tokens)
        {
            Id = id;
            Role = role;
            Credentials = credentials;
            _tokens = tokens;
        }

        public static Result<User> Create(Guid id, UserRole role, AuthCredentials credentials, List<Tokens> tokens)
        {
            if (credentials == null)
            {
                return Result.Failure<User>("empty credentials");
            }

            return Result.Success(new User(id, role, credentials, tokens));
        }

        public static Result<User> Create(UserRole role, AuthCredentials credentials, List<Tokens> tokens)
        {
            if (credentials == null)
            {
                return Result.Failure<User>("empty credentials");
            }

            var id = Guid.NewGuid();

            return Result.Success(new User(id, role, credentials, tokens));
        }

    }
}
