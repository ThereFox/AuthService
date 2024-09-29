using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITokenParser
    {
        public Result<AuthTokenPayload> GetInfoFromAuthToken(AuthorisationToken token);
        public Result<object> GetInfoFromRefreshToken(RefreshToken token);
    }
}
