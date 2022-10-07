using Gateway.API.Logic.RabbitMQ;
using Gateway.API.Logic.Request;
using Microsoft.Extensions.Configuration;

namespace MRA.Gateway.Repository
{
    public class RepositoryBase
    {
        public RequestManager Request;
        public RabbitProducer Rabbit;

        public RepositoryBase(string apiUrl, IConfiguration configuration)
        {
            Request = new RequestManager(apiUrl);
            Rabbit = new RabbitProducer(configuration);
        }
    }
}
