using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesRegger.Data
{
    class Reg
    {

        public static string GetLog(int var = 8)
        {
            string pass = "";
            var r = new Random();
            while (pass.Length < var)
            {
                Char c = (char)r.Next(33, 125);
                if (Char.IsLetterOrDigit(c))
                    pass += c;
            }
            return pass;
        }
        public static async Task RegAcc(string log, string pass)
        {
            string ret = "";
            WebRequest request = WebRequest.Create("https://account.my.games/signup_email/");
            request.Method = "POST"; // для отправки используется метод Post
                                     // данные для отправки
            string data = $"client_id=&continue=https%3A%2F%2Faccount.my.games%2Foauth2%2Fsignup%2F&lang=ru_RU&adId=0&email={log}@monlide.sexy&password={pass}&method=email&verification_continue=https%3A%2F%2Faccount.my.games%2Femail_verify%2F%3Fok%3D1&verification_letter_id=2724";
            // преобразуем данные в массив байтов
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            // устанавливаем тип содержимого - параметр ContentType
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:96.0) Gecko/20100101 Firefox/96.0");
            request.Headers.Add("Cookie", "amc_lang=ru_RU");
            // Устанавливаем заголовок Content-Length запроса - свойство ContentLength
            request.ContentLength = byteArray.Length;

            //записываем данные в поток запроса
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            WebResponse response = await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    ret = reader.ReadToEnd();
                }
            }
            response.Close();
            if(ret.Contains("\"status\":\"ok\""))
                Console.WriteLine($"Успешно!\nЛогин: {log}@monlide.sexy\nПароль: {pass}");
            else
                Console.WriteLine($"Ошибка\n{ret}");
        }
    }
}
