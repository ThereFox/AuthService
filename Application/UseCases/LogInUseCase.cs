using Application.Interfaces;
using Application.Interfaces.Getter;
using Application.Interfaces.TokenOnUser;
using Application.Stores;
using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class AuthentificateUseCase
    {
        private readonly IUserStore _userStore;
        private readonly ITokenGenerator _generator;
        private readonly ITokenStore _tokenStore;
        private readonly ITokenSetter _updater;

        public AuthentificateUseCase(IUserStore userStore, ITokenGenerator generator, ITokenStore tokenStore, ITokenSetter updater)
        {
            _userStore = userStore;
            _generator = generator;
            _tokenStore = tokenStore;
            _updater = updater;
        }

        public async Task<Result> Authintificate(AuthCredentials credentials)
        {
            var getUserByCredentialResult = await _userStore.GetByCredentialInfo(credentials);

            if (getUserByCredentialResult.IsFailure)
            {
                return getUserByCredentialResult;
            }

            var tokens = _generator.Generate(getUserByCredentialResult.Value);

            await _tokenStore.SaveTokens(tokens);
            _updater.SetTokensForCurrentUser(tokens.TokensPair);

            return Result.Success();
        }
    }
}
