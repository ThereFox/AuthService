using Application.Interfaces;
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
    public class GetInfoByTokenUseCase
    {
        private readonly RefreshTokenUseCase _refreshTokenUseCase;
        private readonly ITokenParser _tokenParser;
        private readonly ITokenStore _tokenStore;
        private readonly IUserStore _userStore;

        public GetInfoByTokenUseCase(
            RefreshTokenUseCase refresh,
            ITokenParser tokenParser,
            ITokenStore tokenStore,
            IUserStore userStore)
        {
            _refreshTokenUseCase = refresh;
            _tokenParser = tokenParser;
            _tokenStore = tokenStore;
            _userStore = userStore;
        }
        public async Task<Result<UserShortInfo>> GetInfo(AuthorisationToken auth, RefreshToken token)
        {
            var checkTokenActiveResult = await _tokenStore.IsActiveToken(auth.Token);

            if (checkTokenActiveResult.IsFailure)
            {
                return checkTokenActiveResult.ConvertFailure<UserShortInfo>();
            }

            if (checkTokenActiveResult.Value == false)
            {
                return Result.Failure<UserShortInfo>("current token disabled");
            }

            var getInfoFromAuthTokenResult = await getInfoFromAuthToken(auth);

            if (getInfoFromAuthTokenResult.IsSuccess)
            {
                return getInfoFromAuthTokenResult.Value;
            }

            var refreshTokens = _refreshTokenUseCase.RefreshToken(token);

            if (refreshTokens.IsFailure)
            {
                return refreshTokens.ConvertFailure<UserShortInfo>();
            }

            return await getInfoFromAuthToken(refreshTokens.Value.TokensPair.Auth);
        }

        private async Task<Result<UserShortInfo>> getInfoFromAuthToken(AuthorisationToken token)
        {
            var tokenInfo = _tokenParser.GetInfoFromAuthToken(token);

            if (tokenInfo.IsFailure)
            {
                return tokenInfo.ConvertFailure<UserShortInfo>();
            }

            var getUserInfoResult = await _userStore.GetById(tokenInfo.Value.Id);

            if (getUserInfoResult.IsFailure)
            {
                return tokenInfo.ConvertFailure<UserShortInfo>();
            }

            return getUserInfoResult.Value.GetShortInfo();
        }

    }
}
