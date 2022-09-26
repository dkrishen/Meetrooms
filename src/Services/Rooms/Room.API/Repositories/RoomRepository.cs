using Microsoft.EntityFrameworkCore;
using MRA.Rooms.Data;
using MRA.Rooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRA.Rooms.Repositories
{
    public class RoomRepository : RepositoryBase, IRoomRepository
    {
        public RoomRepository(MRARoomsDBContext context) : base(context) { }

        public async Task<Room> GetRoomByIdAsync(Guid id)
        {
            return await context.Rooms
                .Where(r => r.Id == id)
                .SingleOrDefaultAsync().ConfigureAwait(false); ;
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            return await context.Rooms.ToListAsync().ConfigureAwait(false); ;
        }

        public async Task<IEnumerable<Room>> GetRoomsByIdsAsync(IEnumerable<Guid> ids)
        {
            return await context.Rooms
                .Where(r => ids.Contains(r.Id))
                .ToListAsync().ConfigureAwait(false); ;
        }
    }
}
