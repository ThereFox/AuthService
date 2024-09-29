using Application.Stores;
using AuntificationService.Domain.Entitys;
using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.Stores
{
    public class UserStore : IUserStore
    {
        public async Task<Result<User>> GetByCredentialInfo(AuthCredentials credentials)
        {
            return Result.Failure<User>("Not realised");
            throw new NotImplementedException();
        }

        public async Task<Result<User>> GetById(Guid id)
        {
            return Result.Failure<User>("Not realised");
            throw new NotImplementedException();
        }

        public async Task<Result> SaveNew(User user)
        {
            return Result.Failure("Not realised");
            throw new NotImplementedException();
        }
    }
}
