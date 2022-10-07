using MRA.Gateway.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRA.Gateway.Repository
{
    public class MeetingRoomRepository : RepositoryBase , IMeetingRoomRepository
    {
        public MeetingRoomRepository(IConfiguration configuration) 
            : base(configuration.GetSection("MRA.Rooms").GetValue<string>("Url"), configuration)
        {
        }

        public async Task<IEnumerable<MeetingRoom>> GetAllRoomsAsync(string token)
        {
            return await Request.Get
                .SendAsync<IEnumerable<MeetingRoom>>("api/rooms/GetAllRooms", token)
                .ConfigureAwait(false);
        }

        public async Task<MeetingRoom> GetRoomByRoomIdAsync(Guid id, string token)
        {
            return await Request.Get
                .SendAsync<MeetingRoom>("api/rooms/GetRoomById, token, id")
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<MeetingRoom>> GetRoomsByRoomIdsAsync(IEnumerable<Guid> ids, string token)
        {
            return await Request.Get
                .SendAsync<IEnumerable<MeetingRoom>>("api/rooms/GetRoomsByIds", token, ids)
                .ConfigureAwait(false);
        }
    }
}
