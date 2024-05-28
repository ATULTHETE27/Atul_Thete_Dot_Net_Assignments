using Newtonsoft.Json;

namespace Library_Management_System.Model
{
    public class Issue
    {
        [JsonProperty("id")]
        public string Id { get; set; } // Document identifier
        [JsonProperty("uId")]
        public string UId => Id; // Partition key
        [JsonProperty("bookId")]
        public string BookId { get; set; }
        [JsonProperty("memberId")]
        public string MemberId { get; set; }
        [JsonProperty("issueDate")]
        public DateTime IssueDate { get; set; }
        [JsonProperty("returnDate")]
        public DateTime? ReturnDate { get; set; }
        [JsonProperty("isReturned")]
        public bool IsReturned { get; set; }
    }
}
