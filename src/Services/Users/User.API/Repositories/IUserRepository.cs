using MRA.Users.Models;
using System;
using System.Collections.Generic;

namespace MRA.Users.Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<UserShortDto> GetUsersByIds(IEnumerable<string> ids);
        public IEnumerable<UserShortDto> GetAllUserNames();
    }
}
