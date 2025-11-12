namespace CallbackAPI.Models
{
    public class CallbackRequestModel
    {
        public required string Id { get; set; }
        public required string Status { get; set; }
        public required string Message { get; set; }
    }
}