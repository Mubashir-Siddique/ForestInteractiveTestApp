namespace ForestInteractiveTestApp.Models
{
    public class Recepient
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string MessageId { get; set; }
        public bool IsSent { get; set; }
    }

    public class ApiResponseContent
    {
        public string dnis { get; set; }
        public string message_id { get; set; }
    }
}
