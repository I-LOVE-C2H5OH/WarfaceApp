using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyGamesRegger.Data
{
    class WarfaceAccount
    {
        private List<Cookie> cookie = new List<Cookie>();
        private List<string> NickNames = new List<string>();
        private string n_js_d = "";
        private string n_js_t = "";
        private string FirstName = "";
        public WarfaceAccount(string login, string password, string n_js_d, string n_js_t)
        {
            this.n_js_d = n_js_d;
            this.n_js_t = n_js_t;
            AuthWarfaceAcc wfauth;
            if (login.Contains("@"))
            {
                if (login.Contains("@mail.ru") || login.Contains("@bk.ru") || login.Contains("@inbox.ru") || login.Contains("@list.ru") || login.Contains("@internet.ru"))
                {
                    wfauth = new AuthMailRu(login, password);
                }
                else
                    wfauth = new AuthMyGames(login, password);
                cookie = wfauth.GetWarfaceCookies();
                FirstName = wfauth.GetFisrstName();
                NickNames = SetNickNames();
            }
            else
            { 
            }

        }
        public string Promo(int clas)
        {
            //
            HttpContent content = new StringContent($"reward_num={clas}");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            string cookie = "";
            for (int i = 0; i < this.cookie.Count - 1; i++)
            {
                cookie += this.cookie[i].name + "=" + this.cookie[i].value + "; ";
            }
            cookie += this.cookie[this.cookie.Count - 1].name + "=" + this.cookie[this.cookie.Count - 1].value;
            cookie += $"; n_js_d={n_js_d}; n_js_t={n_js_t}";
            var request = RequestPOST(HttpMethod.Post, new Uri("https://ru.warface.com/dynamic/promo-liquidation/?a=pick"), content, cookie);
            request.Headers.Add("Accept", "application/json, text/plain, */*");
            request.Headers.Add("Origin", "https://ru.warface.com");
            request.Headers.Add("Referer", "https://ru.warface.com/promo/liquidation");
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.Headers.Host = "ru.warface.com";
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
            HttpResponseMessage response = client.Send(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized) { return "Обновите n_js_t и n_js_d"; }
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            if (text.Contains("\"error\":true"))
                return "Уже получено";
            else
                return "Получено";
        }
        public string GetName()
        {
            return FirstName;
        }
        public List<string> GetNickNames()
        {
            return NickNames;
        }
        public bool? Hidden()
        {
            //
            string cookie = "";
            for (int i = 0; i < this.cookie.Count - 1; i++)
            {
                cookie += this.cookie[i].name + "=" + this.cookie[i].value + "; ";
            }
            cookie += this.cookie[this.cookie.Count - 1].name + "=" + this.cookie[this.cookie.Count - 1].value;
            cookie += $"; n_js_d={n_js_d}; n_js_t={n_js_t}";
            var request = RequestGET(HttpMethod.Post, new Uri("https://ru.warface.com/dynamic/user/?a=getapiblacklist"), cookie);
            request.Headers.Host = "ru.warface.com";
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
            HttpResponseMessage response = client.Send(request);
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            if (text.Contains("\"status\":0"))
                return false;
            else if (text.Contains("\"status\":1"))
                return true;
            return false;
            
        }
        public bool SwichHidden()
        {
            //
            string cookie = "";
            for (int i = 0; i < this.cookie.Count - 1; i++)
            {
                cookie += this.cookie[i].name + "=" + this.cookie[i].value + "; ";
            }
            cookie += this.cookie[this.cookie.Count - 1].name + "=" + this.cookie[this.cookie.Count - 1].value;
            cookie += $"; n_js_d={n_js_d}; n_js_t={n_js_t}";
            var request = RequestGET(HttpMethod.Post, new Uri("https://ru.warface.com/dynamic/user/?a=setapiblacklist"), cookie);
            request.Headers.Host = "ru.warface.com";
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
            HttpResponseMessage response = client.Send(request);
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            if (text.Contains("\"status\":0"))
                return false;
            else if (text.Contains("\"status\":1"))
                return true;
            return false;
        }
        List<string> SetNickNames()
        {
            string cookie = "";
            for (int i = 0; i < this.cookie.Count - 1; i++)
            {
                cookie += this.cookie[i].name + "=" + this.cookie[i].value + "; ";
            }
            cookie += this.cookie[this.cookie.Count - 1].name + "=" + this.cookie[this.cookie.Count - 1].value;
            var request = RequestGET(HttpMethod.Post, new Uri("https://ru.warface.com/dynamic/profile/?a=profile"), cookie);
            request.Headers.Host = "ru.warface.com";
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
            HttpResponseMessage response = client.Send(request);
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            Regex regex = new Regex("js-tab\">(.*?)</div>");
            MatchCollection matches = regex.Matches(text);
            List<string> WarfaceName = new List<string>();
            foreach (Match match in matches)
            {
                WarfaceName.Add(match.Groups[1].Value);
            }
            return WarfaceName;
        }
        public void Set_n_js(string n_js_t, string n_js_d)
        { this.n_js_t = n_js_t; this.n_js_d = n_js_d; }
        public string REF()
        {
            string cookie = "";
            for (int i = 0; i < this.cookie.Count - 1; i++)
            {
                cookie += this.cookie[i].name + "=" + this.cookie[i].value + "; ";
            }
            cookie += this.cookie[this.cookie.Count - 1].name + "=" + this.cookie[this.cookie.Count - 1].value;
            cookie += $"; n_js_t={n_js_t}; n_js_d={n_js_d}";
            string testURI = "https://ru.warface.com/dynamic/promo-bounty/?a=reward&code=snowstorm";
            HttpContent content = new StringContent("");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var request = RequestGET(HttpMethod.Get, new Uri(testURI), cookie);
            request.Headers.Host = "ru.warface.com";
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
            HttpResponseMessage response = client.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) { return "Обновите n_js_d"; }
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            if (text.Contains("\"error\":false"))
            {
                return "успех";
            }
            else
                return "неуспех";
        }


        public string GetAccVip(int verTest)
        {
            string cookie = "";
            for (int i = 0; i < this.cookie.Count - 1; i++)
            {
                cookie += this.cookie[i].name + "=" + this.cookie[i].value + "; ";
            }
            cookie += this.cookie[this.cookie.Count - 1].name + "=" + this.cookie[this.cookie.Count - 1].value;
            cookie += $"; n_js_t={n_js_t}; n_js_d={n_js_d}";
            string testURI = "";
            string testOtvet = "";
            if (verTest == 1)
            {
                testURI = "https://ru.warface.com/dynamic/tests/?a=tests";
                testOtvet = $"65=190&64=187&70=209&73=215&74=217&75=220&71=211&69=207&67=199&68=202&66=194&61=177&62=180&72=213&63=184";
            }
            else if (verTest == 2)
            {

                testURI = "https://ru.warface.com/dynamic/tests/?a=phonetests";
                testOtvet = $"9=26&4=9&8=21&2=5&1=2&6=17&3=7&10=29&5=14&7=18";
            }
            else
            {
                return "Неверно указан параметр";
            }

            HttpContent content = new StringContent(testOtvet);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var request = RequestPOST(HttpMethod.Post, new Uri(testURI), content, cookie);

            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            request.Headers.Host = "ru.warface.com";
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
            HttpResponseMessage response = client.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) { return "Обновите n_js_d"; }
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            if (text.Contains("Вы успешно прошли тестирование"))
            {
                return "успех";
            }
            else
                return "неуспех";
        }
        public string RegisterWarface()
        {
            string cookie = "";
            for (int i = 0; i < this.cookie.Count - 1; i++)
            {
                cookie += this.cookie[i].name + "=" + this.cookie[i].value + "; ";
            }
            cookie += this.cookie[this.cookie.Count - 1].name + "=" + this.cookie[this.cookie.Count - 1].value;
            HttpContent content = new StringContent($"");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var request = RequestPOST(HttpMethod.Post, new Uri("https://ru.warface.com/dynamic/auth/?confirm_reg=true&subscribe=true"), content, cookie);
            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            request.Headers.Host = "ru.warface.com";
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
            HttpResponseMessage response = client.Send(request);
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd(); //<span class="username">u_1212331544</span>Личный кабинет
            Regex regex = new Regex("<span class=\"username\">(.*?)</span>");
            MatchCollection matches = regex.Matches(text);

            return matches[0].Groups[1].Value;
        }
        public string GetAccinfo()
        {
            string cookie = "";
            for (int i = 0; i < this.cookie.Count - 1; i++)
            {
                cookie += this.cookie[i].name + "=" + this.cookie[i].value + "; ";
            }
            cookie += this.cookie[this.cookie.Count - 1].name + "=" + this.cookie[this.cookie.Count - 1].value;
            HttpContent content = new StringContent("");
            var request = RequestGET(HttpMethod.Get, new Uri("https://ru.warface.com/dynamic/auth/?profile_reload=0"), cookie);
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
            request.Headers.Host = "ru.warface.com";
            HttpResponseMessage response = client.Send(request);
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            if (text.Contains("Зарегистрироваться"))
            {
                text = RegisterWarface();
            }
            else
            {
                Regex regex = new Regex("<span class=\"username\">(.*?)</span>");
                MatchCollection matches = regex.Matches(text);

                text = matches[0].Groups[1].Value;
            }
            return text;
        }
        HttpRequestMessage RequestPOST(HttpMethod httpMethod, Uri path, HttpContent content, string cookie = "amc_lang=ru_RU")
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = path,
                Method = httpMethod,
                Content = content
            };
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:96.0) Gecko/20100101 Firefox/96.0");
            request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Origin", "https:account.my.games");
            request.Headers.Add("DNT", "1");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Cookie", cookie);
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Sec-Fetch-Dest", "document");
            request.Headers.Add("Sec-Fetch-Mode", "navigate");
            request.Headers.Add("Sec-Fetch-Site", "same-site");
            return request;
        }
        HttpRequestMessage RequestGET(HttpMethod httpMethod, Uri path, string cookie = "amc_lang=ru_RU")
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = path,
                Method = httpMethod
            };

            request.Headers.Add("Host", "account.my.games");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:96.0) Gecko/20100101 Firefox/96.0");
            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Origin", "https:account.my.games");
            request.Headers.Add("DNT", "1");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Cookie", cookie);
            request.Headers.Add("Referer", "https://account.my.games/");
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Sec-Fetch-Dest", "document");
            request.Headers.Add("Sec-Fetch-Mode", "navigate");
            request.Headers.Add("Sec-Fetch-Site", "same-site");
            return request;
        }
    }
}
