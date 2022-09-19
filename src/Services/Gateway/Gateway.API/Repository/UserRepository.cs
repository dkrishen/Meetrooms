using MRA.Gateway.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace MRA.Gateway.Repository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : 
            base(configuration.GetSection("MRA.Users").GetValue<string>("Url"))
        {
        }

        public IEnumerable<UserShortDto> GetUsersByIds(IEnumerable<Guid> ids, string token)
        {
            return Request.Get.Send<IEnumerable<UserShortDto>>("api/user/GetUserNamesByIds", token, ids);
        }
    }
}
