using AuntificationService.Domain.Entitys;
using Domain.Entitys;
using Persistense.DatabaseEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.Convertations.ToDomain
{
    internal static class FromDomainExtension
    {
        internal static UserEntity ToDTO(this User input)
        {
            return new UserEntity(
                input.Id,
                input.Role.RoleId,
                input.Credentials.Login,
                input.Credentials.PasswordHash,
                input.OwnedTokens != default ? input.OwnedTokens.ToList().ConvertAll(ex => ex.ToDTO()) : []
            );
        }

        internal static TokenEntity ToDTO(this Tokens input)
        {
            return new TokenEntity(
                input.Id,
                input.IsDisabled,
                input.CreateDate,
                input.TokensPair.AuthToken.Token.Value,
                input.TokensPair.RefreshToken.Token.Value,
                input.Owner.Id,
                input.Owner != default ? input.Owner.ToDTO() : null
            );
        }


    }
}
