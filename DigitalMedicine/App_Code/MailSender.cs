using System;
using System.Text;
using System.Net.Mail;
using System.Xml.Linq;
using DigitalMedicine.Models;
using DigitalMedicine.Models.Users;

namespace DigitalMedicine.App_Code
{
    public class MailSender
    {
        public enum UserType { Patient, Doctor }

        private MailAddress sender;
        private MailAddress recipient;
        private SmtpClient smtpClient;

        public string Recipient
        {
            set { recipient = new MailAddress(value); }
        }

        public MailSender(string recipientEmail)
        {
            try
            {
                XDocument xdoc = XDocument.Load(@"D:\поликлиника\диплом\DigitalMedicine\Web.config");
                XElement item = xdoc.Element("configuration").Element("system.net").Element("mailSettings").Element("smtp").Element("network");
                if (item == null)
                    throw new Exception("В конфигурационном файле Web.config не содержатся нужные теги для почтовых настроек.(mailSettings.smtp.network)");
                string host = (string)item.Attribute("host"),
                    userName = (string)item.Attribute("userName"),
                    password = (string)item.Attribute("password");
                int port = (int)item.Attribute("port");
                bool ssl = (bool)item.Attribute("enableSsl");
                sender = new MailAddress(userName, "Веб-приложение \"Электроная больница\"",Encoding.UTF8);
                smtpClient = new SmtpClient(host, port);
                smtpClient.Credentials=new System.Net.NetworkCredential(userName, password);
                smtpClient.EnableSsl = ssl;
                recipient = new MailAddress(recipientEmail);
            }
            catch (InvalidCastException ex)
            {

            }
            catch (Exception ex)
            {

            }
        }

        public void SendMessage(string subject, string body,bool isBodyHtml=false)
        {
            MailMessage m = new MailMessage(sender, recipient);
            m.Subject = subject;
            m.Body = body;
            m.IsBodyHtml = isBodyHtml;
            smtpClient.Send(m);
        }

        public void SendMessageForConfirmAccount(string url)
        {
            string subject = "Подтверждение регистрации в приложении \"Электронная больница\"",
                body = string.Format("Для завершения регистрации перейдите по ссылке:" +
                            $"<a href=\"{url}\" title=\"Подтвердить регистрацию\">{url}</a>");
            SendMessage(subject,body,true);
        }

        public void SendMessageForConfirmAccount(string url,string password )
        {
            string subject = "Подтверждение регистрации в приложении \"Электронная больница\"",
                body = string.Format($"Ваш пароль по умолчанию: {password}  . Рекомендуем в ближайшее время сменить его.<br/>" +
                "Для завершения регистрации перейдите по ссылке:" +
                            $"<a href=\"{url}\" title=\"Подтвердить регистрацию\">{url}</a>");
            SendMessage(subject, body, true);
        }

        public void SendMessageForMakeAppointment(User u,DateTime day, TimeSpan time, UserType type,string url="")
        {
            string subject="",body = "";
            if (type == UserType.Doctor)
            {
                subject = "Запись пациента";
                body = $"К вам был записан пациент {u.GetFio(false)}<br/>" +
                    $"День приёма: {day.ToString("d")}<br/>" +
                    $"Время приёма: {time.Hours}:{time.Minutes}";
            }
            else 
            {
                subject = "Запись к доктору";
                body = $"Вы были записаны к доктору (<a href =\"{url}\" title=\"Перейти на страницу доктора\">{u.GetFio(false)})</a> на приём.<br/>" +
                    $"День приёма: {day.ToString("d")}<br/>" +
                    $"Время приёма: {time.Hours}:{time.Minutes}";
            }
            SendMessage(subject, body, true);
        }
    }
}