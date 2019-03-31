using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using OpenPop.Mime;
using OpenPop.Pop3;

namespace Lab4
{
    class Program
    {
        static void SendMail()
        {
            Console.Clear();
            MailAddress to = new MailAddress("lab4prcorn@gmail.com");

            MailAddress from = to;

            MailMessage mail = new MailMessage(from, to);

            mail.Subject = "Lab 4 PR test";

            mail.Body = "This is a test";

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;

            smtp.Credentials = new NetworkCredential(
                from.Address, "L2bP@tru4");
            smtp.EnableSsl = true;
            Console.WriteLine("Sending email...");
            smtp.Send(mail);
            Console.ReadKey();
        }

        static void GetMail()
        {
            Console.Clear();
            Pop3Client pop3 = new Pop3Client();

            pop3.Connect("pop.gmail.com", 995, true);
            pop3.Authenticate("lab4prcorn@gmail.com", "L2bP@tru4");

            //GetMessage(i), i - nr. mesajului pe care il scoatem, de la cel mai vechi la nou
            Message message = pop3.GetMessage(3);
            Console.WriteLine("Subject: {0}", message.Headers.Subject);
            Console.WriteLine("Date Sent: {0}", message.Headers.DateSent);
            Console.WriteLine("From: {0}", message.Headers.From.Address);
            var body = message.FindFirstHtmlVersion();
            if (body != null)
            {
                Console.WriteLine(body.GetBodyAsText());
            }
            else
            {
                body = message.FindFirstPlainTextVersion();

                if (body != null)
                {
                    Console.WriteLine(body.GetBodyAsText());
                }
            }
            Console.ReadKey();
            return;
        }

        static void Main()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Send Mail");
                Console.WriteLine("2. Get Mail");
                Console.WriteLine("0. Exit");

                int mailSwitch = Convert.ToInt32(Console.ReadLine());
                switch (mailSwitch)
                {
                    case 1:
                        SendMail();
                        break;
                    case 2:
                        GetMail();
                        break;
                    case 0:
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Try Again");
                        Console.ReadKey();
                        break;

                }
            }

        }
    }
}
