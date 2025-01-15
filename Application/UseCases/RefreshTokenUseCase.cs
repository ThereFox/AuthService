using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.TokenOnUser;
using Application.Stores;

namespace Application.UseCases
{
    public class RefreshTokenUseCase
    {
        private readonly ITokenSetter _setter;
        private readonly IUserStore _store;
        private readonly ITokenParser _parser;
        private readonly ITokenGenerator _generator;

        public RefreshTokenUseCase(ITokenSetter setter, IUserStore store, ITokenParser parser, ITokenGenerator generator )
        {
            _setter = setter;
            _store = store;
            _parser = parser;
            _generator = generator;
        }

        internal async Task<Result<Tokens>> RefreshToken(RefreshToken token)
        {
            try
            {
                var parseRefreshToken =  _parser.GetInfoFromRefreshToken(token);

                if (parseRefreshToken.IsFailure)
                {
                    return parseRefreshToken.ConvertFailure<Tokens>();
                }

                var ownerId = parseRefreshToken.Value.UserId;

                var getUserFromStoreResult = await _store.GetById(ownerId);

                if (getUserFromStoreResult.IsFailure)
                {
                    return getUserFromStoreResult.ConvertFailure<Tokens>();
                }
                
                var newToken = _generator.Generate(getUserFromStoreResult.Value);
                
                _setter.SetTokensForCurrentUser(newToken.TokensPair);

                return Result.Success(newToken);
            }
            catch (Exception e)
            {
                return Result.Failure<Tokens>(e.Message);
            }
        }
    }
}
