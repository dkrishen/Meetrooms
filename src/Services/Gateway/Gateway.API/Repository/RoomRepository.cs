using MRA.Gateway.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRA.Gateway.Repository
{
    public class RoomRepository : RepositoryBase , IRoomRepository
    {
        public RoomRepository(IConfiguration configuration) 
            : base(configuration.GetSection("MRA.Rooms").GetValue<string>("Url"), configuration)
        {
        }

        public async Task<IEnumerable<MeetingRoom>> GetAllRoomsAsync(string token)
        {
            return await Request.Get
                .SendAsync<IEnumerable<MeetingRoom>>("api/rooms/AllRooms", token)
                .ConfigureAwait(false);
        }

        public async Task<MeetingRoom> GetRoomByRoomIdAsync(Guid id, string token)
        {
            return await Request.Get
                .SendAsync<MeetingRoom>("api/rooms/RoomById, token, id")
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<MeetingRoom>> GetRoomsByRoomIdsAsync(IEnumerable<Guid> ids, string token)
        {
            return await Request.Get
                .SendAsync<IEnumerable<MeetingRoom>>("api/rooms/RoomsByIds", token, ids)
                .ConfigureAwait(false);
        }
    }
}
