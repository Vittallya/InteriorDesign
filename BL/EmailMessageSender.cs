using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace BL
{
    public class EmailMessageSender : IEmailMessageSender
    {
        public string ErrorMessage { get; private set; }

        public int SendIntervalInSec { get; } = 10;

        public async Task<bool> SendCode(string code, string emailReciever)
        {
            MailAddress from = new MailAddress("dizaynintererov@mail.ru", "Дизайн интерьеров");
            // кому отправляем
            MailAddress to = new MailAddress(emailReciever);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Проверочный код";
            // текст письма
            m.Body = $"<h2 align=center>Проверочный код: {code}</h2>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("dizaynintererov@mail.ru", "eVOYYmrui42(");
            smtp.EnableSsl = true;

            try
            {
                await smtp.SendMailAsync(m);
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

            return true;
        }
    }
}
