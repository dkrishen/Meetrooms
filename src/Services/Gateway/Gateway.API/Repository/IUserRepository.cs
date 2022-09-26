using MRA.Gateway.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRA.Gateway.Repository
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserShortDto>> GetUsersByIdsAsync(IEnumerable<Guid> ids, string token);
    }
}
