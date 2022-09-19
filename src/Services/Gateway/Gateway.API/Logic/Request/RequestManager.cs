namespace Gateway.API.Logic.Request
{
    public class RequestManager
    {
        public GetRequest Get;
        public PostRequest Post;
        public PutRequest Put;
        public DeleteRequest Delete;
        
        public RequestManager(string url)
        {
            this.Get = new GetRequest(url);
            this.Post = new PostRequest(url);
            this.Put = new PutRequest(url);
            this.Delete = new DeleteRequest(url);
        }
    }
}
