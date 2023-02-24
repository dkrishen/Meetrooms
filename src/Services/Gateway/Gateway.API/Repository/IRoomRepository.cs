using MRA.Gateway.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRA.Gateway.Repository
{
    public interface IRoomRepository
    {
        public Task<IEnumerable<MeetingRoom>> GetAllRoomsAsync(string token);
        public Task<MeetingRoom> GetRoomByRoomIdAsync(Guid id, string token);
        public Task<IEnumerable<MeetingRoom>> GetRoomsByRoomIdsAsync(IEnumerable<Guid> id, string token);
    }
}
