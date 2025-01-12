using Application.InputObjects;
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
using AuntificationService.Domain.Entitys;
using AuntificationService.Domain.ValueObjects;

namespace Application.UseCases
{
    public class RegistrateUseCase
    {
        private readonly IUserStore _userStore;
        private readonly ITokenSetter _setter;
        private readonly ITokenGenerator _generator;

        public RegistrateUseCase(IUserStore userStore, ITokenGenerator generator, ITokenSetter setter)
        {
            _userStore = userStore;
            _setter = setter;
            _generator = generator;
        }

        public async Task<Result<Tokens>> Registrate(RegistrateInputObject input)
        {
            var validateCredentials = AuthCredentials.Create(input.Login, input.Password);

            if (validateCredentials.IsFailure)
            {
                return Result.Failure<Tokens>(validateCredentials.Error);
            }
            
            var validateUser = User.Create(Guid.NewGuid(), UserRole.Simple, validateCredentials.Value, []);

            if (validateUser.IsFailure)
            {
                return Result.Failure<Tokens>(validateUser.Error);
            }
            
            var saveResult = await _userStore.SaveNew(validateUser.Value);

            if (saveResult.IsFailure)
            {
                return Result.Failure<Tokens>(saveResult.Error);
            }
            
            var newTokens = _generator.Generate(validateUser.Value);
            
            _setter.SetTokensForCurrentUser(newTokens.TokensPair);

            return Result.Success(newTokens);
        }
    }
}
