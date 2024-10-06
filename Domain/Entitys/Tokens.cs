using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuntificationService.Domain.Entitys;
using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Domain.ValueObjects;

namespace Domain.Entitys
{
    public class Tokens : Entity<Guid>
    {
        public bool IsDisabled { get; private set; }
        public DateTime CreateDate { get; init; }
        public TokensPair TokensPair { get; init; }

        public User Owner { get; private set; }

        private Tokens(Guid id, bool isDisabled, DateTime createDate, TokensPair tokens, User owner)
        {
            Id = id;
            IsDisabled = isDisabled;
            CreateDate = createDate;
            TokensPair = tokens;
            Owner = owner;
        }

        public static Result<Tokens> Create(Guid id, bool isDisabled, DateTime createDate, TokensPair tokens, User owner)
        {
            if(tokens == null)
            {
                return Result.Failure<Tokens>("Empty tokens");
            }

            return Result.Success(new Tokens(id, isDisabled, createDate, tokens, owner));
        }
        public static Result<Tokens> Create(bool isDisabled, DateTime createDate, TokensPair tokens, User owner)
        {
            if (tokens == null)
            {
                return Result.Failure<Tokens>("Empty tokens");
            }

            var id = Guid.NewGuid();

            return Result.Success(new Tokens(id, isDisabled, createDate, tokens, owner));
        }
        public static Result<Tokens> Create(DateTime createDate, TokensPair tokens, User owner)
        {
            if (tokens == null)
            {
                return Result.Failure<Tokens>("Empty tokens");
            }

            var id = Guid.NewGuid();

            return Result.Success(new Tokens(id, false, createDate, tokens, owner));
        }

        public Result Deactivate()
        {
            if (IsDisabled)
            {
                return Result.Failure("Token already disabled");
            }
            IsDisabled = true;
            return Result.Success();
        }

    }
}
