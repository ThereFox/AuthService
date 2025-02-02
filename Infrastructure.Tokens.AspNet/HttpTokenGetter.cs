﻿using Application.Interfaces.Getter;
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
using Infrastructure.Tokens.JWT.Converters;

namespace Infrastructure.Tokens.Getter
{
    public class HttpTokenGetter : ITokenGetter, ITokenSetter
    {
        private readonly IHttpContextAccessor _contextAcsessor;
        private readonly string _headerName;
        private readonly string _cookieName;

        public HttpTokenGetter(IHttpContextAccessor contextAcsessor, string headerName, string cookieName)
        {
            _contextAcsessor = contextAcsessor;
            _headerName = headerName;
            _cookieName = cookieName;
        }


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

            var validateAuthToken = authTokenRaw.ToAuthorisationToken();
            var ValidaterefreshToken = refreshTokenRaw.ToRefreshToken();

            if (validateAuthToken.IsFailure || ValidaterefreshToken.IsFailure)
            {
                return Result.Failure<TokensPair>("request dont contain a valid tokens");
            }
            
            var pair = TokensPair.Create(validateAuthToken.Value, ValidaterefreshToken.Value);
            
            return pair;
        }

        public void SetTokensForCurrentUser(TokensPair tokens)
        {
            var response = _contextAcsessor.HttpContext.Response;
            
            response.Headers.Add(_headerName, tokens.AuthToken.Token.Value);
            response.Cookies.Append(_cookieName, tokens.RefreshToken.Token.Value, new CookieOptions()
            {
                Expires = DateTimeOffset.MaxValue,
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                Path = "/admin"
            });

        }

        public void RemoveTokensFromCurrentUser()
        {
            var context = _contextAcsessor.HttpContext;
            
            context.Response.Cookies.Delete(_cookieName);
            context.Response.Headers.Remove(_headerName);
        }
    }
}
