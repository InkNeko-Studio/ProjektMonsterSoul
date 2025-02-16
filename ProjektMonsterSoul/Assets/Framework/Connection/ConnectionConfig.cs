using Newtonsoft.Json;

namespace Framework.Connection
{
    public static class ConnectionConfig
    {
        public static int Port = 4444;
        public static int BytesRead = 256;
        public static JsonSerializerSettings JsonSettings = new JsonSerializerSettings()
        {
            CheckAdditionalContent = false,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
    }
}