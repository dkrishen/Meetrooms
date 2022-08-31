using MRA.Bookings.Data;

namespace MRA.Bookings.Repositories
{
    public class RepositoryBase
    {
        protected MRABookingsDbContext context;
        public RepositoryBase(MRABookingsDbContext context)
            => this.context = context;
    }
}
