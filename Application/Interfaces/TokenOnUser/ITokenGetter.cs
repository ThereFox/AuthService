using CSharpFunctionalExtensions;
using Domain.Entitys;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Getter
{
    public interface ITokenGetter
    {
        public Result<TokensPair> GetTokensFromCurrentUser();
    }
}
