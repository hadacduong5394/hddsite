namespace HD.Service.Interface
{
    public interface IEmailService
    {
        void SendEmail(string emailTo, string subject, string content);
    }
}