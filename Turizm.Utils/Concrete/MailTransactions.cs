using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Turizm.Business.Concrete;
using Turizm.DataAccess.Concrete.EntityFramework;
using Turizm.Entities.Concrete;
using Turizm.Utils.Abstract;

namespace Turizm.Utils.Concrete
{
    public class MailTransactions : ISender
    {
        private const string MAIL = "*****"; // Buraya kendi mailimizi yazıyoruz.
        private const string PASSWORD = "*****"; // Buraya da mailin şifresini veriyiroz.

        private TourManager _tourManager = new TourManager(new EfTourDal());

        public static int SendForgotPasswordMail(string mail)
        {
            Random ran = new Random();
            int mailSifresi = ran.Next(1000, 9999);
  
            MailMessage msj = new MailMessage();
            SmtpClient istemci = new SmtpClient("smtp-mail.outlook.com");
            istemci.Credentials = new System.Net.NetworkCredential(MAIL, PASSWORD);
            istemci.Port = 587;
            //istemci.Host = "smtp-mail.outlook.com";
            // outlook ile bu göndermek için kodumuz budur.
            // gmail ile istenirse smtp.gmail.com ile sağlanabilir.
            // bunun için google hesabınızın düşük güvenlik modunun açılması lazımdır!
            istemci.EnableSsl = true;
            msj.To.Add(mail);
            msj.From = new MailAddress(MAIL);
            msj.Subject = "Şifre Yenileme";
            msj.Body = "Şifre Yenilemek için kodunuz : " + mailSifresi.ToString();

            istemci.Send(msj);

            return mailSifresi;   
        }
        public static void SendTourRegisterMail(Customer customer)
        {
            MailTransactions m = new MailTransactions();
            Tour tour = m._tourManager.GetWithName(customer.RegisteredTour);
            MailMessage msj = new MailMessage();
            SmtpClient istemci = new SmtpClient("smtp-mail.outlook.com");
            istemci.Credentials = new System.Net.NetworkCredential(MAIL, PASSWORD);
            
            istemci.Port = 587;
            //istemci.Host = "smtp-mail.outlook.com";
            // outlook ile bu göndermek için kodumuz budur.
            // gmail ile istenirse smtp.gmail.com ile sağlanabilir.
            // bunun için google hesabınızın düşük güvenlik modunun açılması lazımdır!
            istemci.EnableSsl = true;
            msj.To.Add(customer.Mail);
            msj.From = new MailAddress(MAIL);
            msj.Subject = "Compass Turizm";
            msj.Body = "Sayın "+ customer.FirstName+" "+customer.LastName+" "+customer.RegisteredTour + " isimli tura kaydoldunuz.\n --------------------------------Turumuz Hakkında--------------------------------\n\n"
                + tour.Description+ " \n\n\n --------------------------------\nDetaylı Bilgi İçin : * *** *** **** \n --------------------------------";

            istemci.Send(msj);
        }
    }
}
