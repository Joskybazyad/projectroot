using System.Net;
using System.Net.Mail;

namespace projectroot.Utilities
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("bazyad666@gmail.com", "xmoivgfafqvnpnyy")
            };

            client.Send("bazyad666@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
