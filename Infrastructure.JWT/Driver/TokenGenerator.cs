using Application.Interfaces;
using AuntificationService.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tokens.JWT.Driver
{
    public class TokenGenerator : ITokenGenerator
    {
        public Domain.Entitys.Tokens Generate(User user)
        {
            return null;
            throw new NotImplementedException();
        }
    }
}
