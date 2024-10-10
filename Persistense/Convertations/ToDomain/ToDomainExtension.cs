using AuntificationService.Domain.Entitys;
using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Domain.Entitys;
using Domain.ValueObjects;
using Persistense.DatabaseEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.Convertations.ToDomain
{
    internal static class ToDomainExtension
    {
        internal static Result<User> ToDomain(this UserEntity input)
        {
            var validateUserRole = UserRole.Create(input.RoleId);

            if (validateUserRole.IsFailure)
            {
                return Result.Failure<User>(validateUserRole.Error);
            }

            var validateCredentials = AuthCredentials.Create(input.Login, input.Password);

            if (validateCredentials.IsFailure)
            {
                return Result.Failure<User>(validateCredentials.Error);
            }


            var validateUserResult = User.Create(input.Id, validateUserRole.Value, validateCredentials.Value, null);

            return validateUserResult;
        }
        internal static Result<Tokens> ToDomain(this TokenEntity input)
        {
            var authTokenCommonValidateResult = Token.Create(input.AuthToken);
            var refreshTokenCommonValidateResult = Token.Create(input.RefreshToken);

            if (authTokenCommonValidateResult.IsFailure || refreshTokenCommonValidateResult.IsFailure)
            {
                return Result.Failure<Tokens>("invalid tokens format");
            }

            var AuthTokenValidateResult = AuthorisationToken.Create(authTokenCommonValidateResult.Value);

            if (AuthTokenValidateResult.IsFailure)
            {
                return Result.Failure<Tokens>(authTokenCommonValidateResult.Error);
            }

            var RefreshTokenValidateResult = RefreshToken.Create(authTokenCommonValidateResult.Value);

            if (RefreshTokenValidateResult.IsFailure)
            {
                return Result.Failure<Tokens>(authTokenCommonValidateResult.Error);
            }


            var tokensPairValidateResult = TokensPair.Create(AuthTokenValidateResult.Value, RefreshTokenValidateResult.Value);

            if (tokensPairValidateResult.IsFailure)
            {
                return tokensPairValidateResult.ConvertFailure<Tokens>();
            }

            var validateUserResult = input.Owner.ToDomain();

            if (validateUserResult.IsFailure)
            {
                return validateUserResult.ConvertFailure<Tokens>();
            }

            var tokens = Tokens.Create(input.Id, input.IsDisabled, input.CreateDate, tokensPairValidateResult.Value, validateUserResult.Value);

            return tokens;
        }
    }
}
