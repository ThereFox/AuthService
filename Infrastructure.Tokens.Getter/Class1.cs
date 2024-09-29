using Application.Interfaces.Getter;
using Application.Interfaces.TokenOnUser;
using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tokens.Getter
{
    public class HttpTokenGetter : ITokenGetter, ITokenSetter
    {
        private readonly IHttpContextAccessor _contextAcsessor;
        private readonly string _headerName;
        private readonly string _cookieName;


        public Result<TokensPair> GetTokensFromCurrentUser()
        {
            var context = _contextAcsessor.HttpContext;

            if(context.Request.Headers.ContainsKey(_headerName) == false)
            {
                return Result.Failure<TokensPair>("request dont contain a auth token");
            }
            if (context.Request.Cookies.ContainsKey(_cookieName) == false)
            {
                return Result.Failure<TokensPair>("request dont contain a refresh token");
            }

            var authTokenRaw = context.Request.Headers[_headerName].First();
            var refreshTokenRaw = context.Request.Cookies[_cookieName];

            return Result.Failure<TokensPair>("NotRealised");
            //var authToken = AuthorisationToken

        }

        public void SetTokensForCurrentUser(TokensPair tokens)
        {
            throw new NotImplementedException();
        }

    }
}
