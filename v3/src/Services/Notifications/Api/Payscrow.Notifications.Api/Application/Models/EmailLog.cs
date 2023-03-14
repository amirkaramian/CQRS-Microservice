namespace Payscrow.Notifications.Api.Application.Models
{
    public class EmailLog : BaseModel
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsSent { get; set; }
    }
}