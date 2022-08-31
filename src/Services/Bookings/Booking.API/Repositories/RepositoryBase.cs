using MRA.Bookings.Data;

namespace MRA.Bookings.Repositories
{
    public class RepositoryBase
    {
        protected MRABooksDbContext context;
        public RepositoryBase(MRABooksDbContext context)
            => this.context = context;
    }
}
