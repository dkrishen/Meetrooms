using Microsoft.EntityFrameworkCore;
using MRA.Users.Data;
using MRA.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRA.Users.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(MRAIdentityDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UserShortDto>> GetAllUserNamesAsync()
        {
            return await context.Users
                .Select(u => new UserShortDto() { Id = u.Id, Username = u.UserName })
                .ToListAsync().ConfigureAwait(false); ;
        }

        public async Task<IEnumerable<UserShortDto>> GetUsersByIdsAsync(IEnumerable<string> ids)
        {
            return await context.Users
                .Select(u => new UserShortDto() { Id = u.Id, Username = u.UserName })
                .Where(u => ids.Contains(u.Id))
                .ToListAsync().ConfigureAwait(false); ;
        }
    }
}
