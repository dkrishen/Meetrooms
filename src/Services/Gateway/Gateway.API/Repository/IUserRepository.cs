using MRA.Gateway.Models;
using System;
using System.Collections.Generic;

namespace MRA.Gateway.Repository
{
    public interface IUserRepository
    {
        public IEnumerable<UserShortDto> GetUsersByIds(IEnumerable<Guid> ids, string token);
    }
}
