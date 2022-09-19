using MRA.Gateway.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace MRA.Gateway.Repository
{
    public class MeetingRoomRepository : RepositoryBase , IMeetingRoomRepository
    {
        public MeetingRoomRepository(IConfiguration configuration) 
            : base(configuration.GetSection("MRA.Rooms").GetValue<string>("Url"))
        {
        }

        public IEnumerable<MeetingRoom> GetAllRooms(string token)
        {
            return Request.Get.Send<IEnumerable<MeetingRoom>>("api/rooms/GetAllRooms", token);
        }

        public MeetingRoom GetRoomByRoomId(Guid id, string token)
        {
            return Request.Get.Send<MeetingRoom>("api/rooms/GetRoomById, token, id");
        }

        public IEnumerable<MeetingRoom> GetRoomsByRoomIds(IEnumerable<Guid> ids, string token)
        {
            return Request.Get.Send<IEnumerable<MeetingRoom>>("api/rooms/GetRoomsByIds", token, ids);
        }
    }
}
