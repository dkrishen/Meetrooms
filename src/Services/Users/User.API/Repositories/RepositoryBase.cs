using MRA.Users.Data;

namespace MRA.Users.Repositories
{
    public class RepositoryBase
    {
        protected MRAIdentityDBContext context;
        public RepositoryBase(MRAIdentityDBContext context)
            => this.context = context;
    }
}
