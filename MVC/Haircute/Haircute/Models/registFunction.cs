using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Haircute.Models;
using System.Security.Cryptography;
using System.Globalization;

namespace Haircute.Models
{
    public class registFunction
    {
        public async Task SendEmail(string FID, string Email)
        {
            string 加密ID = encryptstr(FID);
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("alan825444@gmail.com", "Example User");
            var to = new EmailAddress(Email, "Example User");
            var subject = "Haircute認證信件";
            var plainTextContent = "請按以下連結認證信箱";
            var htmlContent = $"<h3>請點選以下連結開通設計師全縣</h3>";
            htmlContent += $"<a href='https://localhost:44333/Home/mailtest/{加密ID}'>認證連結</a>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public byte[] encrypt(string string_secretContent)
        {
            //密碼轉譯一定都是用byte[] 所以把string都換成byte[]  
            byte[] byte_secretContent = Encoding.UTF8.GetBytes(string_secretContent);
            byte[] byte_pwd = Encoding.UTF8.GetBytes("haircuteisawesome");
            //加解密函數的key通常都會有固定的長度 而使用者輸入的key長度不定 因此用hash過後的值當做key  
            MD5CryptoServiceProvider provider_MD5 = new MD5CryptoServiceProvider();
            byte[] byte_pwdMD5 = provider_MD5.ComputeHash(byte_pwd);
            //產生加密實體 如果要用其他不同的加解密演算法就改這裡(ex:AES)  
            RijndaelManaged provider_AES = new RijndaelManaged();
            ICryptoTransform encrypt_AES = provider_AES.CreateEncryptor(byte_pwdMD5, byte_pwdMD5);
            //output就是加密過後的結果  
            byte[] output = encrypt_AES.TransformFinalBlock(byte_secretContent, 0, byte_secretContent.Length);
            return output;
        }
        //解密函式  
        public string decrypt(byte[] byte_ciphertext)
        {
            //密碼轉譯一定都是用byte[] 所以把string都換成byte[]  
            byte[] byte_pwd = Encoding.UTF8.GetBytes("haircuteisawesome");
            //加解密函數的key通常都會有固定的長度 而使用者輸入的key長度不定 因此用hash過後的值當做key  
            MD5CryptoServiceProvider provider_MD5 = new MD5CryptoServiceProvider();
            byte[] byte_pwdMD5 = provider_MD5.ComputeHash(byte_pwd);
            //產生解密實體  
            RijndaelManaged provider_AES = new RijndaelManaged();
            ICryptoTransform decrypt_AES = provider_AES.CreateDecryptor(byte_pwdMD5, byte_pwdMD5);
            //string_secretContent就是解密後的明文  
            byte[] byte_secretContent = decrypt_AES.TransformFinalBlock(byte_ciphertext, 0, byte_ciphertext.Length);
            string string_secretContent = Encoding.UTF8.GetString(byte_secretContent);
            return string_secretContent;
        }

        public string encryptstr (string str)
        {
            Random r = new Random();
            int[] ramdon3 = new int[3];

            while (str.Length <13)
            {
                str += " ";
            }
            ramdon3[0] = r.Next(0, 9);
            ramdon3[1] = r.Next(10, 99);
            ramdon3[2] = r.Next(1000, 9999);
            string nstr = ramdon3[0] + str.Substring(0, 3) + ramdon3[1] + str.Substring(3, 5) + ramdon3[2] + str.Substring(8, 5);
            byte[] b = encrypt(nstr);
            string encryptStr = Convert.ToBase64String(b);
            byte[] newBytes = Convert.FromBase64String(encryptStr);
            return BitConverter.ToString(newBytes);
        }


        public string decryptstr(string str)
        {
            //轉換hex字串
            string sHex = str;

            // Split into the individual bytes. Each two digit hex number is one byte.
            string[] byteStrings = sHex.Split(new char[1] { '-' });

            // Now cycle through the strings and convert each one to a byte.Allocate
            // the byte array first.
            byte[] bytes = new byte[byteStrings.Length];

            // Cycle through the strings.
            for (int index = 0; index < bytes.Length; index++)
            {
                // Perform the conversion.
                bytes[index] = byte.Parse(byteStrings[index],
                NumberStyles.AllowHexSpecifier);
            }

            string base64 = Convert.ToBase64String(bytes);
            //解密  
            byte[] b = Convert.FromBase64String(base64);
            string newString = decrypt(b);
            //拆掉亂數  
            string decryptStr = newString.Substring(1, 3) + newString.Substring(6, 5) + newString.Substring(15, 5);
            return decryptStr;
        }

        public string 忘記密碼(string Email) 
        {
            demodbEntities db = new demodbEntities();
            var q = db.tMember.Where(x => x.fEmail == Email).FirstOrDefault();
            if (q != null)
            {
                密碼重設信件(q.fID.ToString(), q.fEmail).Wait();
                return "OK";
            }
            return "Error";
        }

        public async Task 密碼重設信件(string FID, string Email)
        {
            string 加密ID = encryptstr(FID);
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("alan825444@gmail.com", "Example User");
            var to = new EmailAddress(Email, "Example User");
            var subject = "Haircute認證信件";
            var plainTextContent = "請按以下連結認證信箱";
            var htmlContent = $"<h3>請點選以下連結重設密碼</h3>";
            htmlContent += $"<a href='https://localhost:44333/Home/PwdReset/{加密ID}'>重設密碼</a>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public string 重設密碼功能(string id,string Pwd, string CheckPwd) 
        {
            demodbEntities db = new demodbEntities();
            var ID = Convert.ToInt32(id);
            var q = db.tMember.Where(x => x.fID == ID).FirstOrDefault();
            if (Pwd == CheckPwd)
            {
                q.fPwd = Pwd;
                db.SaveChanges();
                return "OK";
            }
            return "Error";
        }
    }
}