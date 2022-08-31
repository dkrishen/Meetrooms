using MRA.Gateway.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MRA.Gateway.Repository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration.GetSection("MRA.Users").GetValue<string>("Url"))
        {
        }

        public IEnumerable<UserShortDto> GetUsersByIds(IEnumerable<Guid> ids, string token)
        {
            var jsonResponse = Request("api/user/GetUserNamesByIds?data=", "GET", ids, token);
            return JsonConvert.DeserializeObject<IEnumerable<UserShortDto>>(jsonResponse);
        }
    }
}
