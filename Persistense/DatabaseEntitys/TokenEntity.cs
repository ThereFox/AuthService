using AuntificationService.Domain.Entitys;
using Domain.Entitys;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.DatabaseEntitys
{
    public sealed class TokenEntity
    {
        public Guid Id { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime CreateDate { get; init; }
        public string AuthToken { get; init; }
        public string RefreshToken { get; init; }
        
        public Guid OwnerId {  get; set; }
        
        public UserEntity Owner { get; set; }

        public TokenEntity(Guid id, bool isDisabled, DateTime createDate, string authToken, string refreshToken, Guid ownerId, UserEntity owner)
        {
            Id = id;
            IsDisabled = isDisabled;
            CreateDate = createDate;
            AuthToken = authToken;
            RefreshToken = refreshToken;
            OwnerId = ownerId;
            Owner = owner;
        }

        private TokenEntity()
        {
            
        }


    }
}
