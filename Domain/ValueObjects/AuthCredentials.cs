using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuntificationService.Domain.ValueObjects
{
    public class AuthCredentials : ValueObject
    {
        public string Login {  get; init; }
        public string PasswordHash { get; init; }

        private AuthCredentials(string login, string passwordHash)
        {
            Login = login;
            PasswordHash = passwordHash;
        }

        public static Result<AuthCredentials> Create(string login, string password)
        {
            if (String.IsNullOrWhiteSpace(login))
            {
                return Result.Failure<AuthCredentials>("empty login");
            }
            if (String.IsNullOrWhiteSpace(password))
            {
                return Result.Failure<AuthCredentials>("empty password");
            }

            var passwordHash = Encoding.UTF8.GetString(MD5.HashData(Encoding.ASCII.GetBytes(login)));

            return Result.Success(new AuthCredentials(login, passwordHash));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Login;
            yield return PasswordHash;
        }
    }
}
