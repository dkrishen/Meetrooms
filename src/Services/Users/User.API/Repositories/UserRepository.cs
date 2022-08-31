using MRA.Users.Data;
using MRA.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MRA.Users.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(MRAIdentityDBContext context) : base(context)
        {
        }

        public IEnumerable<UserShortDto> GetAllUserNames()
        {
            return context.Users.Select(u => new UserShortDto() { Id = u.Id, Username = u.UserName });
        }

        public IEnumerable<UserShortDto> GetUsersByIds(IEnumerable<string> ids)
        {
            return context.Users.Select(u => new UserShortDto() { Id = u.Id, Username = u.UserName }).Where(u => ids.Contains(u.Id));
        }
    }
}
