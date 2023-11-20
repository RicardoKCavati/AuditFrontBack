using SecurityDriven.Inferno;
using System.Net;
using System.Net.Mail;

namespace AuditApp.Server.Services
{
    public class EmailService
    {
        private const string MyEmail = "buscajogosplus@gmail.com";
        private const string MyPassword = "lnhrxkbpkcwgcrfs";

        private static void SendMail(string subject, string body, string receiverEmail)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Credentials = new NetworkCredential(MyEmail, MyPassword)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(MyEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(receiverEmail);

            smtpClient.Send(mailMessage);
        }

        public void SendResetPasswordEmailMail(string receiverEmail, int changePasswordCode, DateTime expirationDate)
        {
            var body = $"<h2>Sua senha foi reinicializada, utilize este código quando solicitado para criar uma nova.</h2>" +
                $"<br><h3>{changePasswordCode}</h3><br>Este código é válido até {expirationDate.ToShortTimeString()}";

            SendMail("Reinicialização de senha", body, receiverEmail);
        }

        public void SendMfaCodeToLogin(string receiverEmail, out int mfaCode, out DateTime expirationDate)
        {
            mfaCode = new CryptoRandom().Next(100_000, 999_999);

            expirationDate = DateTime.Now.AddMinutes(5);

            var body = $"<h2>Segue o código para realizar o login.</h2>" +
                $"<br><h3>{mfaCode}</h3><br>Este código é válido até {expirationDate.ToShortTimeString()}";

            SendMail("Código MFA", body, receiverEmail);
        }
    }
}
