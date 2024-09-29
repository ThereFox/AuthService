using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuntificationService.Domain.ValueObjects
{
    public class UserRole : ValueObject
    {
        public static UserRole Simple => new(1);
        public static UserRole Admin => new(2);
        private static readonly List<UserRole> _all = [Simple, Admin];


        public int RoleId { get; init; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return RoleId;
        }

        protected UserRole(int roleId)
        {
            RoleId = roleId;
        }

        public static Result<UserRole> Create(int roleId)
        {
            if(_all.Any(ex => ex.RoleId == roleId) == false)
            {
                return Result.Failure<UserRole>("Unsupported");
            }

            return Result.Success<UserRole>(new UserRole(roleId));
        }
    }
}
