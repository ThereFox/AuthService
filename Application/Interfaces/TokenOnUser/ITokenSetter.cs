using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using AuntificationService.Domain;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Application.Interfaces.TokenOnUser
{
    public interface ITokenSetter
    {
        public void SetTokensForCurrentUser(TokensPair tokens);
        public void RemoveTokensFromCurrentUser();
    }
}
