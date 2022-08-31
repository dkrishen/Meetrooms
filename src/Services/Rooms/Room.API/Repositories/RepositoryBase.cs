using MRA.Rooms.Data;
using MRA.Rooms.Models;

namespace MRA.Rooms.Repositories
{
    public class RepositoryBase
    {
        protected MRARoomsDBContext context;
        public RepositoryBase(MRARoomsDBContext context) 
            => this.context = context;
    }
}
