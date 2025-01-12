using Application.Stores;
using CSharpFunctionalExtensions;
using Domain.Entitys;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistense.DTOs;

namespace Persistense.Stores
{
    public class TokenStore : ITokenStore
    {
        private readonly ApplicationDBContext _dbcontext;

        public TokenStore(ApplicationDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Result> DeactivateTokens(Tokens tokens)
        {
            if (tokens == null)
            {
                return Result.Failure("null tokens");
            }

            if(await _dbcontext.Database.CanConnectAsync() == false)
            {
                return Result.Failure("database unawaliable");
            }

            try
            {
                var getTokenFromDBResult = await this.GetById(tokens.Id);

                if (getTokenFromDBResult.IsFailure)
                {
                    return getTokenFromDBResult.ConvertFailure();
                }

                _dbcontext.Remove(getTokenFromDBResult.Value);

                await _dbcontext.SaveChangesAsync();

                return Result.Success();
            }
            catch(Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
        public async Task<Result> DeactivateTokens(Guid tokensId)
        {

            if (await _dbcontext.Database.CanConnectAsync() == false)
            {
                return Result.Failure("database unawaliable");
            }

            try
            {
                var getTokenFromDBResult = await this.GetById(tokensId);

                if (getTokenFromDBResult.IsFailure)
                {
                    return getTokenFromDBResult.ConvertFailure();
                }

                _dbcontext.Remove(getTokenFromDBResult.Value);

                await _dbcontext.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> IsActiveToken(Token token)
        {
            if(await _dbcontext.Database.CanConnectAsync() == false)
            {
                return Result.Failure<bool>("database unawaliable");
            }

            try
            {
                var recordByToken = await _dbcontext
                    .Tokens
                    .AsNoTracking()
                    .FirstOrDefaultAsync(ex => ex.RefreshToken == token.Value || ex.AuthToken == token.Value);

                if(recordByToken == default)
                {
                    return Result.Failure<bool>($"dont contain record with token {token.Value}");
                }

                return Result.Success(true);

                //throw new NotImplementedException();

                //return Result.Success(recordByToken.IsDisabled == false);
            }
            catch (Exception ex)
            {
                return Result.Failure<bool>(ex.Message);
            }

        }

        public async Task<Result<Tokens>> GetById(Guid id)
        {
            if (await _dbcontext.Database.CanConnectAsync() == false)
            {
                return Result.Failure<Tokens>("database unawaliable");
            }

            try
            {
                var token = await _dbcontext
                    .Tokens
                    .AsNoTracking()
                    .FirstOrDefaultAsync(ex => ex.Id == id);

                if(token == default)
                {
                    return Result.Failure<Tokens>($"database dont contain token with id {id}");
                }


                return Converters.ToDomain(token);
            }
            catch (Exception ex)
            {
                return Result.Failure<Tokens>(ex.Message);
            }
        }

        public async Task<Result> SaveTokens(Tokens tokens)
        {
            if(await _dbcontext.Database.CanConnectAsync() == false)
            {
                return Result.Failure("database unawaliable");
            }

            try
            {
                await _dbcontext.AddAsync(tokens.ToDTO());

                await _dbcontext.SaveChangesAsync();

                return Result.Success();

            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
