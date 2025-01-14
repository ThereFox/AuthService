using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;

namespace Domain.ValueObjects
{
    public class AuthTokenPayload : ValueObject
    {
        public Guid UserId { get; set; }
        public UserRole Role { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
        }

        private AuthTokenPayload(Guid userId, UserRole role)
        {
            UserId = userId;
            Role = role;
        }

        public static Result<AuthTokenPayload> Create(Guid userId, UserRole role)
        {
            if (userId.Equals(Guid.Empty))
            {
                return Result.Failure<AuthTokenPayload>("UserId is invalid");
            }

            return Result.Success(new AuthTokenPayload(userId, role));
        }
    }
}
