using Gateway.API.Logic.Request;

namespace MRA.Gateway.Repository
{
    public class RepositoryBase
    {
        public RequestManager Request;

        public RepositoryBase(string apiUrl)
        {
            Request = new RequestManager(apiUrl);
        }
    }
}
