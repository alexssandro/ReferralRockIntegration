namespace ReferralRockIntegration.Service.Models.Notification
{
    public class Notification
    {
        public Notification(string field, string message)
        {
            Message = message;
            Field = field;
        }

        public string Message { get; }
        public string Field { get; }
    }
}
