using Newtonsoft.Json;

namespace Library_Management_System.Model
{
    public class Book
    {
        [JsonProperty("id")]
        public string Id { get; set; } // Document identifier
        [JsonProperty("uId")]
        public string UId => Id; // Partition key
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("publishedDate")]
        public DateTime PublishedDate { get; set; }
        [JsonProperty("isbn")]
        public string ISBN { get; set; }
        [JsonProperty("isIssued")]
        public bool IsIssued { get; set; }
    }
}
