using CSharpFunctionalExtensions;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class LogOutUseCase
    {

        public Result LogOut(Tokens tokens)
        {
            return Result.Failure("");
        }
    }
}
