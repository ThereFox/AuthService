using Application.InputObjects;
using CSharpFunctionalExtensions;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class RegistrateUseCase
    {
        public Result<Tokens> Registrate(RegistrateInputObject input)
        {
            return Result.Failure<Tokens>("");
        }
    }
}
