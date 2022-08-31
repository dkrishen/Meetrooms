using MRA.Gateway.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MRA.Gateway.Repository
{
    public class MeetingRoomRepository : RepositoryBase , IMeetingRoomRepository
    {
        public MeetingRoomRepository(IConfiguration configuration) : base(configuration.GetSection("MRA.Rooms").GetValue<string>("Url"))
        {
        }

        public IEnumerable<MeetingRoom> GetAllRooms(string token)
        {
            var jsonResponse = Request("api/rooms/GetAllRooms", "GET", token: token);
            return JsonConvert.DeserializeObject<IEnumerable<MeetingRoom>>(jsonResponse);
        }

        public MeetingRoom GetRoomByRoomId(Guid id, string token)
        {
            var jsonResponse = Request("api/rooms/GetRoomById?data=", "GET", id, token);
            return JsonConvert.DeserializeObject<MeetingRoom>(jsonResponse);
        }

        public IEnumerable<MeetingRoom> GetRoomsByRoomIds(IEnumerable<Guid> ids, string token)
        {
            var jsonResponse = Request("api/rooms/GetRoomsByIds?data=", "GET", ids, token);
            return JsonConvert.DeserializeObject<IEnumerable<MeetingRoom>>(jsonResponse);
        }
    }
}
