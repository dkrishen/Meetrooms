using MRA.Rooms.Data;
using MRA.Rooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MRA.Rooms.Repositories
{
    public class RoomRepository : RepositoryBase, IRoomRepository
    {
        public RoomRepository(MRARoomsDBContext context) : base(context) { }

        public Room GetRoomById(Guid id)
        {
            return context.Rooms.Where(r => r.Id == id).SingleOrDefault();
        }

        public IEnumerable<Room> GetRooms()
        {
            return context.Rooms.ToList();
        }

        public IEnumerable<Room> GetRoomsByIds(IEnumerable<Guid> ids)
        {
            return context.Rooms.Where(r => ids.Contains(r.Id)).ToList();
        }
    }
}
