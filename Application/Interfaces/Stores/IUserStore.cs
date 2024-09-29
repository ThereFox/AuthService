using AuntificationService.Domain.Entitys;
using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stores
{
    public interface IUserStore
    {
        public Task<Result<User>> GetByCredentialInfo(AuthCredentials credentials);
        public Task<Result<User>> GetById(Guid id);
        public Task<Result> SaveNew(User user);
    }
}
