using Newtonsoft.Json;

namespace WebApplication1
{
    public class User
    {
     
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}