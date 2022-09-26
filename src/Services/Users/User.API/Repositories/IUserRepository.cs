using MRA.Users.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRA.Users.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserShortDto>> GetUsersByIdsAsync(IEnumerable<string> ids);
        public Task<IEnumerable<UserShortDto>> GetAllUserNamesAsync();
    }
}
