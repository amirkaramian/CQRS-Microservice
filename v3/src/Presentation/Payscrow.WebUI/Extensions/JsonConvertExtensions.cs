using Newtonsoft.Json;

namespace Payscrow.WebUI
{
    public static class JsonConvertHelper
    {
        public static object TryDeserialize(string data)
        {
            try
            {
                return JsonConvert.DeserializeObject(data);
            }
            catch
            {
                return data;
            }
        }
    }
}