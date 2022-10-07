using Gateway.API.Logic.RabbitMQ;
using Gateway.API.Logic.Request;

namespace MRA.Gateway.Repository
{
    public class RepositoryBase
    {
        public RequestManager Request;
        public RabbitProducer Rabbit;

        public RepositoryBase(string apiUrl)
        {
            Request = new RequestManager(apiUrl);
            Rabbit = new RabbitProducer();
        }
    }
}
