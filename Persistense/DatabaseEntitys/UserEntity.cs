using AuntificationService.Domain.Entitys;
using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.DatabaseEntitys
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public int RoleId { get; set; }
        public string Login { get; set; }
        public string Password {  get; set; }

        public List<TokenEntity> Tokens { get; set; }

        public UserEntity(Guid id, int roleId, string login, string password, List<TokenEntity> tokens)
        {
            Id = id;
            RoleId = roleId;
            Login = login;
            Password = password;
            Tokens = tokens;
        }

        private UserEntity()
        {
            
        }
    }
}
