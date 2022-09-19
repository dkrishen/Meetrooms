using Newtonsoft.Json;

namespace Gateway.API.Logic.Extenshions
{
    public static class ObjectExtenshion
    {
        public static string SerializeToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
