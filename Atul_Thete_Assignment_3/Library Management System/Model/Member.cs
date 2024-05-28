using Newtonsoft.Json;

namespace Library_Management_System.Model
{
    public class Member
    {
        [JsonProperty("id")]
        public string Id { get; set; } // Document identifier
        [JsonProperty("uId")]
        public string UId => Id; // Partition key
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
