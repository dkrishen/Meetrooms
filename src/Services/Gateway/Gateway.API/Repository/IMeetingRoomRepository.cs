using MRA.Gateway.Models;
using System;
using System.Collections.Generic;

namespace MRA.Gateway.Repository
{
    public interface IMeetingRoomRepository
    {
        public IEnumerable<MeetingRoom> GetAllRooms(string token);
        public MeetingRoom GetRoomByRoomId(Guid id, string token);
        public IEnumerable<MeetingRoom> GetRoomsByRoomIds(IEnumerable<Guid> id, string token);
    }
}
