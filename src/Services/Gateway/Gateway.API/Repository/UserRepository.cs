using MRA.Gateway.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRA.Gateway.Repository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : 
            base(configuration.GetSection("MRA.Users").GetValue<string>("Url"), configuration)
        {
        }

        public async Task<IEnumerable<UserShortDto>> GetUsersByIdsAsync(IEnumerable<Guid> ids, string token)
        {
            return await Request.Get
                .SendAsync<IEnumerable<UserShortDto>>("api/user/GetUserNamesByIds", token, ids)
                .ConfigureAwait(false);
        }
    }
}
