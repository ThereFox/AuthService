using CSharpFunctionalExtensions;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.TokenOnUser;

namespace Application.UseCases
{
    public class LogOutUseCase
    {

        private readonly ITokenSetter _setter;

        public LogOutUseCase(ITokenSetter setter)
        {
            _setter = setter;
        }

        public Result LogOut()
        {
            try
            {
                _setter.RemoveTokensFromCurrentUser();
                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.Failure(e.Message);
            }
        }
    }
}
