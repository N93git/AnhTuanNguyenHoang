using System;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using TCP.DAL;
using System.Linq;
using TCP.DTO;
namespace Tool
{
   public class Utils
    {
        public static string Getcanonical(string url)
        {
            int rmt = url.IndexOf("?");
            if (rmt != -1)
                url = url.Remove(rmt, url.Length - rmt).ToString();
            return url;
        }
        public static string GetMeta()
        {
            DAL_Settings objSettings = new DAL_Settings();
            string getvalue = objSettings.GETALL(0).FirstOrDefault().Metadecription;
            return getvalue;
        }
        public static string GetLogo()
        {
            DAL_Settings objSettings = new DAL_Settings();
            string getvalue = objSettings.GETALL(0).FirstOrDefault().Logo;
            return getvalue;
        }
        public static string GetFavicon()
        {
            DAL_Settings objSettings = new DAL_Settings();
            string getvalue = objSettings.GETALL(0).FirstOrDefault().Favicon;
            return getvalue;
        }
        public static string GetBanner1()
        {
            DAL_Settings objSettings = new DAL_Settings();
            string getvalue = objSettings.GETALL(0).FirstOrDefault().Banner1;
            return getvalue;
        }
        public static string GetBanner2()
        {
            DAL_Settings objSettings = new DAL_Settings();
            string getvalue = objSettings.GETALL(0).FirstOrDefault().Banner2;
            return getvalue;
        }
        public static string GetURL1()
        {
            DAL_Settings objSettings = new DAL_Settings();
            string getvalue = objSettings.GETALL(0).FirstOrDefault().currencyUSA;
            return getvalue;
        }
        public static string GetURL2()
        {
            DAL_Settings objSettings = new DAL_Settings();
            string getvalue = objSettings.GETALL(0).FirstOrDefault().currencyFrance;
            return getvalue;
        }
        public static string GetTitle()
        {
            DAL_Settings objSettings = new DAL_Settings();
            string getvalue = objSettings.GETALL(0).FirstOrDefault().Title;
            return getvalue;
        }
        public static bool CheckSplit(string split,int check)
        {
            var getlist = split.Split(',');
            foreach(var item in getlist)
            {
                if (item == check.ToString())
                    return true;
            }
            return false;
        }
        public static decimal ReturnPrice(int idproduct)
        {
            DAL_Products objproduct = new DAL_Products();
            var result = objproduct.GETALL("", idproduct, 1, 1, 100).FirstOrDefault();
            if (result.ProductPriceDrop > 0)
                return result.ProductPrice;
            return 0;

        }
        public static String DecryptMD5(String strSource)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider objMD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(strSource);
            bs = objMD5.ComputeHash(bs);
            System.Text.StringBuilder strResult = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                strResult.Append(b.ToString("x2").ToLower());
            }

            return strResult.ToString();
        }
        public static string ConvertToUnSign(string text)
        {
            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            text = text.Replace(" ", "-");

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToLower();
        }
        public static void SendEmail(string _receiver, string _subject, string _content)
        { 

            try
            { 
                string smtpserver = "mail.vitico.vn";
                string smtpEmail = "contact@vitico.vn";
                string smtpPassword = "Vitico@123";
                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.UseDefaultCredentials = false;
                //email va mat khau cua tai khoan de chung thuc viec gui mail
                SmtpServer.Credentials = new System.Net.NetworkCredential(smtpEmail, smtpPassword);
                SmtpServer.Port = 25;//day la cong cua gmail
                SmtpServer.Host = smtpserver;//host gmail, neu cau hinh nen thay the cai nay
                SmtpServer.EnableSsl = false;


                MailMessage mail = new MailMessage();
                String[] addr = _receiver.Split(',');//day la dia chi nguoi nhan mail
                                                     //try
                                                     //{
                mail.From = new MailAddress(smtpEmail, smtpEmail, System.Text.Encoding.UTF8);//nguoi gui
                Byte i;
                for (i = 0; i < addr.Length; i++)
                    mail.To.Add(addr[i]);
                mail.Subject = _subject;
                mail.Body = _content;
                //đính kèm file thì mở code này ra
                //if (fileAttach != "")
                //{
                //    mail.Attachments.Add(new Attachment(fileAttach.ToString()));
                //}
                mail.IsBodyHtml = true;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                SmtpServer.Send(mail);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
