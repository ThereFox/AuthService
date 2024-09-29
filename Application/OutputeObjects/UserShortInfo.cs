using AuntificationService.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuntificationService.Domain.ValueObjects
{
    public record UserShortInfo
    (
        Guid UserId,
        int RoleId ,
        string Email
    );

    public static class GetShortInfoExtension
    {
        public static UserShortInfo GetShortInfo(this User fullInfo)
        {
            return new UserShortInfo(fullInfo.Id, fullInfo.Role.RoleId, null);
        }
    }
}
