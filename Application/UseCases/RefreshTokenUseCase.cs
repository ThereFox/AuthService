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
    public class RefreshTokenUseCase
    {
        internal Result<Tokens> RefreshToken(RefreshToken token)
        {
            return Result.Failure<Tokens>("Not realised");
        }
    }
}
