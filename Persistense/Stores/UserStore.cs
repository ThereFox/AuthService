using Application.Stores;
using AuntificationService.Domain.Entitys;
using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Persistense.Convertations.ToDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.Stores
{
    public class UserStore : IUserStore
    {
        private readonly ApplicationDBContext _dbcontext;

        public UserStore(ApplicationDBContext context)
        {
            _dbcontext = context;
        }

        public async Task<Result<User>> GetByCredentialInfo(AuthCredentials credentials)
        {
            if(await _dbcontext.Database.CanConnectAsync() == false)
            {
                return Result.Failure<User>("database unawaliable");
            }

            try
            {
                var result = await _dbcontext
                    .Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(ex => ex.Login == credentials.Login && ex.Password == credentials.PasswordHash);

                if (result == default)
                {
                    return Result.Failure<User>("Dont have user with this credentials");
                }

                return result.ToDomain();

            }
            catch (Exception ex)
            {
                return Result.Failure<User>(ex.Message);
            }
        }

        public async Task<Result<User>> GetById(Guid id)
        {
            if (await _dbcontext.Database.CanConnectAsync() == false)
            {
                return Result.Failure<User>("database unawaliable");
            }

            try
            {
                var result = await _dbcontext
                    .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(ex => ex.Id == id);

                if (result == default)
                {
                    return Result.Failure<User>("Dont have user with this credentials");
                }

                return result.ToDomain();

            }
            catch (Exception ex)
            {
                return Result.Failure<User>(ex.Message);
            }
        }

        public async Task<Result> SaveNew(User user)
        {
            if (await _dbcontext.Database.CanConnectAsync() == false)
            {
                return Result.Failure<User>("database unawaliable");
            }

            try
            {
                var savableDTO = user.ToDTO();

                await _dbcontext.AddAsync(savableDTO);
                await _dbcontext.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure<User>(ex.Message);
            }
        }
    }
}
