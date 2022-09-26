using MRA.Rooms.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRA.Rooms.Repositories
{
    public interface IRoomRepository
    {
        public Task<IEnumerable<Room>> GetRoomsAsync();
        public Task<IEnumerable<Room>> GetRoomsByIdsAsync(IEnumerable<Guid> ids);
        public Task<Room> GetRoomByIdAsync(Guid id);
    }
}
