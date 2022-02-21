using Microsoft.VisualBasic;
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
    
    internal class AuthMailRu : AuthWarfaceAcc
    {

        private  List<Cookie> cookieMAilRu = new List<Cookie>();
        private  List<Cookie> cookieMyGames = new List<Cookie>();
        private  List<Cookie> cookieWarface = new List<Cookie>();
        private  List<string> WarfaceName = new List<string>();
        private string FirstName = "";
        private string? Phone= "";
        private string Login = "";
        public AuthMailRu(string login, string password)
        {
            Login = login;
            cookieMAilRu = GetActCookie();
            string token = "";
            foreach (Cookie cookie1 in cookieMAilRu)
            {
                if (cookie1.name == "act")
                {
                    token = cookie1.value;
                    break;
                }
            }
            HttpContent content = new StringContent($"login={login}&password={password}&saveauth=1&token={token}&project=e.mail.ru");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            long? l = content.Headers.ContentLength;
            string cookie = "";
            for (int i = 0; i < cookieMAilRu.Count - 1; i++)
            {
                cookie += cookieMAilRu[i].name +"="+ cookieMAilRu[i].value + "; ";
            }
            cookie += cookieMAilRu[cookieMAilRu.Count - 1].name + "=" + cookieMAilRu[cookieMAilRu.Count - 1].value;
            var request = RequestPOST(new Uri("https://auth.mail.ru/jsapi/auth"), content, cookie);
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
            HttpResponseMessage response = client.Send(request);
            IEnumerable<string>? cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            string path = "https://auth-ac.my.games/social/mailru?display=popup&form_params=force_us%3D1%26signup_target%3D_self%26remind_target%3D_self%26logo_target%3D_none%26allow_biz_domains%3Dcorp.my.com%2Cmy.games&continue=https%3A%2F%2Faccount.my.games%2Fsocial_back%2F%3Fcontinue%3Dhttps%253A%252F%252Faccount.my.games%252Foauth2%252F%253Fredirect_uri%253Dhttps%25253A%25252F%25252Fru.warface.com%25252Fdynamic%25252Fauth%25252F%25253Fo2%25253D1%2526client_id%253Dru.warface.com%2526response_type%253Dcode%2526signup_method%253Demail%25252Cphone%2526signup_social%253Dmailru%25252Cfb%25252Cvk%25252Cg%25252Cok%25252Ceg%25252Ctwitch%25252Ctw%25252Csteam%2526lang%253Dru_RU%2526gc_id%253D0.1177%26client_id%3Dru.warface.com%26popup%3D1&failure=https%3A%2F%2Faccount.my.games%2Fsocial_back%2F%3Fsoc_error%3D1%26continue%3Dhttps%253A%252F%252Faccount.my.games%252Foauth2%252Flogin%252F%253Fcontinue%253Dhttps%25253A%25252F%25252Faccount.my.games%25252Foauth2%25252Flogin%25252F%25253Fcontinue%25253Dhttps%2525253A%2525252F%2525252Faccount.my.games%2525252Foauth2%2525252F%2525253Fredirect_uri%2525253Dhttps%252525253A%252525252F%252525252Fru.warface.com%252525252Fdynamic%252525252Fauth%252525252F%252525253Fo2%252525253D1%25252526client_id%2525253Dru.warface.com%25252526response_type%2525253Dcode%25252526signup_method%2525253Demail%252525252Cphone%25252526signup_social%2525253Dmailru%252525252Cfb%252525252Cvk%252525252Cg%252525252Cok%252525252Ceg%252525252Ctwitch%252525252Ctw%252525252Csteam%25252526lang%2525253Dru_RU%25252526gc_id%2525253D0.1177%252526client_id%25253Dru.warface.com%252526lang%25253Dru_RU%252526signup_method%25253Demail%2525252Cphone%252526signup_social%25253Dmailru%2525252Cfb%2525252Cvk%2525252Cg%2525252Cok%2525252Ceg%2525252Ctwitch%2525252Ctw%2525252Csteam%252526gc_id%25253D0.1177%2526client_id%253Dru.warface.com%2526lang%253Dru_RU%2526signup_method%253Demail%25252Cphone%2526signup_social%253Dmailru%25252Cfb%25252Cvk%25252Cg%25252Cok%25252Ceg%25252Ctwitch%25252Ctw%25252Csteam%2526gc_id%253D0.1177";
            if (text == "{\"status\":\"fail\",\"code_number\":409,\"code\":\"auth\"}")
            {
                throw new Exception("Неверный логин/пароль");
            }
            if (text.Contains("https://auth.mail.ru/cgi-bin/secstep"))
            {
                cookies = secstep(cookies);
            }
            if (cookies == null) { }
            else
            {
                foreach (string cook in cookies)
                {
                    Cookie cooks = new Cookie();
                    string[] c = cook.Split("; ")[0].Split("=");
                    cooks.name = c[0];
                    cooks.value = c[1];

                    cookieMAilRu.Add(cooks);
                }
                content = new StringContent($"email={login}&htmlencoded=false");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                cookie = "";
                for (int i = 0; i < cookieMAilRu.Count - 1; i++)
                {
                    cookie += cookieMAilRu[i].name + "=" + cookieMAilRu[i].value + "; ";
                }
                cookie += cookieMAilRu[cookieMAilRu.Count - 1].name + "=" + cookieMAilRu[cookieMAilRu.Count - 1].value;
                
                string[] sss = Regirectes(new string[2], path);
                string o2csrf = GetValue("o2csrf");
                cookie = "";
                for (int i = 0; i < cookieMAilRu.Count - 1; i++)
                {
                    cookie += cookieMAilRu[i].name + "=" + cookieMAilRu[i].value + "; ";
                }
                cookie += cookieMAilRu[cookieMAilRu.Count - 1].name + "=" + cookieMAilRu[cookieMAilRu.Count - 1].value;
                content = new StringContent($"o2csrf={o2csrf}&Page={sss[0]}&FailPage={sss[1]}&browser_data=%7B%22screen%22%3A%7B%22availWidth%22%3A%222048%22%2C%22availHeight%22%3A%221112%22%2C%22width%22%3A%222048%22%2C%22height%22%3A%221152%22%2C%22colorDepth%22%3A%2224%22%2C%22pixelDepth%22%3A%2224%22%2C%22top%22%3A%220%22%2C%22left%22%3A%220%22%2C%22availTop%22%3A%220%22%2C%22availLeft%22%3A%220%22%2C%22mozOrientation%22%3A%22landscape-primary%22%2C%22onmozorientationchange%22%3A%22inaccessible%22%7D%2C%22navigator%22%3A%7B%22doNotTrack%22%3A%221%22%2C%22maxTouchPoints%22%3A%220%22%2C%22oscpu%22%3A%22Windows+NT+10.0%3B+Win64%3B+x64%22%2C%22vendor%22%3A%22%22%2C%22vendorSub%22%3A%22%22%2C%22productSub%22%3A%2220100101%22%2C%22cookieEnabled%22%3A%22true%22%2C%22buildID%22%3A%2220181001000000%22%2C%22webdriver%22%3A%22false%22%2C%22hardwareConcurrency%22%3A%2210%22%2C%22appCodeName%22%3A%22Mozilla%22%2C%22appName%22%3A%22Netscape%22%2C%22appVersion%22%3A%225.0+%28Windows%29%22%2C%22platform%22%3A%22Win32%22%2C%22userAgent%22%3A%22Mozilla%2F5.0+%28Windows+NT+10.0%3B+Win64%3B+x64%3B+rv%3A97.0%29+Gecko%2F20100101+Firefox%2F97.0%22%2C%22product%22%3A%22Gecko%22%2C%22language%22%3A%22ru-RU%22%2C%22onLine%22%3A%22true%22%7D%2C%22flash%22%3A%7B%22version%22%3A%22inaccessible%22%7D%7D&mode=&login={login}");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                request = RequestPOST(new Uri("https://o2.mail.ru/login"), content, cookie);
                httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
                httpClientHandler.AllowAutoRedirect = false;
                client = new HttpClient(httpClientHandler);
                response = client.Send(request);
                if (response.Headers.Location == null) { }
                else
                {
                    string URLAuth = RegirectMyGames(response.Headers.Location.AbsoluteUri);
                    string crftoken = GetCRFTOKEN(URLAuth);
                    cookieWarface = GetCookieWarface(crftoken);
                }
            }
        }

        public List<Cookie> GetWarfaceCookies()
        {
            return this.cookieWarface;
        }
        public string GetFisrstName()
        {
            return this.FirstName;
        }

        IEnumerable<string>? secstep(IEnumerable<string> cookies)
        { 
            
            string cookie = "";
            for (int i = 0; i < cookieMAilRu.Count - 1; i++)
            {
                cookie += cookieMAilRu[i].name + "=" + cookieMAilRu[i].value + "; ";
            }
            cookie += cookieMAilRu[cookieMAilRu.Count - 1].name + "=" + cookieMAilRu[cookieMAilRu.Count - 1].value;
            foreach (string str in cookies)
            {
                cookie += "; "+ str.Split("; ")[0];
            }
            var request = RequestGET(new Uri("https://auth.mail.ru/cgi-bin/secstep?FromAccount=1&send_sms=1"), cookie);
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
            HttpResponseMessage response = client.Send(request);
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            Regex regex = new Regex("\"csrf\":\"(.*?)\"");
            MatchCollection matches = regex.Matches(text);
            string csrf = matches[0].Groups[1].Value;
            regex = new Regex("\"secstep_phone\":\"(.*?)\",");
            matches = regex.Matches(text);
            this.Phone = matches[0].Groups[1].Value;
            IEnumerable<string> cookiess = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            foreach (string cook in cookiess)
            {
                Cookie cooks = new Cookie();
                string[] c = cook.Split("; ")[0].Split("=");
                cooks.name = c[0];
                cooks.value = c[1];
                int i = Contain(cooks, cookieMAilRu);
                if (i != -1)
                {
                    cookieMAilRu[i].value = cooks.value;
                }
                else
                    cookieMAilRu.Add(cooks);
            }
            cookie = "";
            for (int i = 0; i < cookieMAilRu.Count - 1; i++)
            {
                cookie += cookieMAilRu[i].name + "=" + cookieMAilRu[i].value + "; ";
            }
            cookie += cookieMAilRu[cookieMAilRu.Count - 1].name + "=" + cookieMAilRu[cookieMAilRu.Count - 1].value;
            Console.WriteLine($"Введите код для аккаунта {this.Login}\n номер {this.Phone}");
            string? code = Interaction.InputBox("Введите код из СМС", "DualFactoAuntentification");
            if (code == null)
            {
                secstep(cookies);
            }
            HttpContent content = new StringContent($"csrf={csrf}&Login={this.Login}&AuthCode={code}&Permanent=1");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            request = RequestPOST(new Uri("https://auth.mail.ru/cgi-bin/secstep"), content, cookie);
            request.Headers.Clear();
            request.Headers.Add("Host", "auth.mail.ru");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:96.0) Gecko/20100101 Firefox/96.0");
            request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Referer", "https://auth.mail.ru/cgi-bin/secstep?FromAccount=1");
            request.Headers.Add("DNT", "1");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Cookie", cookie);
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Sec-Fetch-Dest", "document");
            request.Headers.Add("Sec-Fetch-Mode", "navigate");
            request.Headers.Add("Sec-Fetch-Site", "same-site");
            httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            client = new HttpClient(httpClientHandler);
            response = client.Send(request);
            cookiess = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            if (cookiess != null)
                foreach (string cook in cookiess)
                {
                    Cookie cooks = new Cookie();
                    string[] c = cook.Split("; ")[0].Split("=");
                    cooks.name = c[0];
                    cooks.value = c[1];
                    int i = Contain(cooks, cookieMAilRu);
                    if (i != -1)
                    {
                        cookieMAilRu[i].value = cooks.value;
                    }
                    else
                        cookieMAilRu.Add(cooks);
                }
            cookie = "";
            for (int i = 0; i < cookieMAilRu.Count - 1; i++)
            {
                cookie += cookieMAilRu[i].name + "=" + cookieMAilRu[i].value + "; ";
            }
            cookie += cookieMAilRu[cookieMAilRu.Count - 1].name + "=" + cookieMAilRu[cookieMAilRu.Count - 1].value;
            if (response.Headers.Location != null)
            {
                request = RequestGET(response.Headers.Location, cookie);
                httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
                httpClientHandler.AllowAutoRedirect = false;
                client = new HttpClient(httpClientHandler);
                response = client.Send(request);
                cookiess = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
                if (response.Headers.Location == null || response.Headers.Location.AbsoluteUri == null) { Console.WriteLine("неверный код"); return secstep(cookies); }
                return cookiess;
            }
            return null;
        }
        

        

        
        
        List<Cookie> GetCookieWarface(string csrfmiddlewaretoken)
        {
            string datas = $"csrfmiddlewaretoken={csrfmiddlewaretoken}&response_type=code&client_id=ru.warface.com&redirect_uri=https%3A%2F%2Fru.warface.com%2Fdynamic%2Fauth%2F%3Fo2%3D1&scope=&state=&hash=be7ced8c2ae834813f503822e744fade&gc_id=0.1177&force=1";
            HttpContent contentes = new StringContent(datas);
            contentes.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            string ccc = "amc_lang=ru_RU";

            foreach (Cookie cook in cookieMyGames)
            {
                ccc += "; " + cook.name + "=" + cook.value;
            }
            var getwarface = RequestPOSTos(new Uri("https://account.my.games/oauth2/"), contentes, ccc);
            HttpClientHandler httpClientHandlers = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandlers.AllowAutoRedirect = false;

            HttpClient clients = new HttpClient(httpClientHandlers);
            HttpResponseMessage responses = clients.Send(getwarface);
            if (responses.StatusCode == HttpStatusCode.BadGateway)
                throw new Exception("Превышено к-во входов");
            IEnumerable<string> scookies = responses.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            List<string> sssscookies = new List<string>();
            foreach (string cookie in scookies)
            {
                sssscookies.Add(cookie.Split("; ")[0]);
            }
            if (responses.Headers.Location != null)
                if (responses.Headers.Location.AbsoluteUri.Contains("ru.warface.com"))
                {
                    return GetWarfaceCookie(responses.Headers.Location.AbsoluteUri);
                }
            return new List<Cookie>();
        }
        List<Cookie> GetWarfaceCookie(string path)
        {
            List<Cookie> cookieWarface = new List<Cookie>();
            HttpContent content = new StringContent("");
            var request = RequestGET(new Uri(path), "");
            //request.Headers.Host = path.Host;
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
            request.Headers.Host = "ru.warface.com";
            HttpResponseMessage response = client.Send(request);
            if (response.StatusCode == HttpStatusCode.BadGateway || (response.Headers.Location != null && response.Headers.Location.OriginalString.Contains("validate")))
                throw new Exception("Превышено к-во входов");
            
            IEnumerable<string> cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            foreach (string cook in cookies)
            {
                Cookie cooks = new Cookie();
                string[] c = cook.Split("; ")[0].Split("=");
                cooks.name = c[0];
                cooks.value = c[1];
                int i = Contain(cooks, cookieWarface);
                if (i != -1)
                {
                    cookieWarface[i].value = cooks.value;
                }
                else
                    cookieWarface.Add(cooks);
            }


            return cookieWarface;
        }
        string GetValue(string name)
        {
            foreach (Cookie cook in cookieMAilRu)
            {
                if (cook.name == name)
                    return cook.value;
            }
                return "";
        }
        string[] Regirectes(string[] strs, string path, int cvo_login = 1)
        {
            var cookie = "";
            for (int i = 0; i < cookieMAilRu.Count - 1; i++)
            {
                cookie += cookieMAilRu[i].name + "=" + cookieMAilRu[i].value + "; ";
            }
            cookie += cookieMAilRu[cookieMAilRu.Count - 1].name + "=" + cookieMAilRu[cookieMAilRu.Count - 1].value;
            var request = RequestGET(new Uri(path), cookie);
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
            HttpResponseMessage response = client.Send(request);
            IEnumerable<string> cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            if (cookies != null)
                foreach (string cook in cookies)
                {
                    Cookie cooks = new Cookie();
                    string[] c = cook.Split("; ")[0].Split("=");
                    cooks.name = c[0];
                    cooks.value = c[1];
                    int i = Contain(cooks, cookieMAilRu);
                    if (i != -1)
                    {
                        cookieMAilRu[i].value = cooks.value;
                    }
                    else
                        cookieMAilRu.Add(cooks);
                }
            if (response.StatusCode == HttpStatusCode.OK)
                return strs;
            else
            {
                if (response.Headers.Location != null)
                    if (response.Headers.Location.AbsoluteUri.Contains("https://o2.mail.ru/login"))
                    {
                        strs[cvo_login] = response.Headers.Location.AbsoluteUri;
                        cvo_login--;
                    }
                    else if (response.Headers.Location.AbsoluteUri.Contains("https://o2.mail.ru/xlogin"))
                    {
                        strs[cvo_login] = response.Headers.Location.AbsoluteUri;
                        cvo_login--;
                    }
            }
            if (response.Headers.Location != null)
                return Regirectes(strs, response.Headers.Location.AbsoluteUri, cvo_login);
            else
                return new string[0];
        }
        string GetCRFTOKEN(string uri)
        {
            string cookie = "";
            for (int i = 0; i < cookieMyGames.Count - 1; i++)
            {
                cookie += cookieMyGames[i].name + "=" + cookieMyGames[i].value + "; ";
            }
            cookie += cookieMyGames[cookieMyGames.Count - 1].name + "=" + cookieMyGames[cookieMyGames.Count - 1].value;
            var request = RequestGET(new Uri(uri), cookie);
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
            HttpResponseMessage response = client.Send(request);
            IEnumerable<string> cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            foreach (string cook in cookies)
            {
                Cookie cooks = new Cookie();
                string[] c = cook.Split("; ")[0].Split("=");
                cooks.name = c[0];
                cooks.value = c[1];
                int i = Contain(cooks, cookieMyGames);
                if (i != -1)
                {
                    cookieMyGames[i].value = cooks.value;
                }
                else
                    cookieMyGames.Add(cooks);
            }
            var stream = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            Regex regex = new Regex("<input type=\"hidden\" name=\"csrfmiddlewaretoken\" value=\"(.*?)\">");
            MatchCollection matches = regex.Matches(text);
            string csrfmiddlewaretoken = matches[0].Groups[1].Value;
            regex = new Regex("<a href=\"//profile.my.games\" target=\"_blank\">(.*?)</a></span>");
            matches = regex.Matches(text);
            FirstName = matches[0].Groups[1].Value;
            return csrfmiddlewaretoken;
        }
        string RegirectMyGames(string path)
        {
            var cookie = "";
            if (cookieMyGames.Count >= 1)
            {
                for (int i = 0; i < cookieMyGames.Count - 1; i++)
                {
                    cookie += cookieMyGames[i].name + "=" + cookieMyGames[i].value + "; ";
                }
                cookie += cookieMyGames[cookieMyGames.Count - 1].name + "=" + cookieMyGames[cookieMyGames.Count - 1].value;
            }
            var request = RequestGET(new Uri(path), cookie);
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
            HttpResponseMessage response = client.Send(request);
            IEnumerable<string> cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            if (cookies != null)
                foreach (string cook in cookies)
                {
                    Cookie cooks = new Cookie();
                    string[] c = cook.Split("; ")[0].Split("=");
                    cooks.name = c[0];
                    cooks.value = c[1];
                    int i = Contain(cooks, cookieMyGames);
                    if(i != -1)
                    {
                        cookieMyGames[i].value = cooks.value;
                    }
                     else
                        cookieMyGames.Add(cooks);
                }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stream = response.Content.ReadAsStream();
                StreamReader reader = new StreamReader(stream);
                string text = reader.ReadToEnd();
                Regex regex = new Regex("<a id=\"continue\" href=\"(.*?)\" class=\"ph-link\">ссылка</a>");
                MatchCollection matches = regex.Matches(text);
                string csrfmiddlewaretoken = matches[0].Groups[1].Value;
                return csrfmiddlewaretoken;
            }
            else
            {

            }
            if (response.Headers.Location != null)
                return RegirectMyGames(response.Headers.Location.AbsoluteUri);
            return "";
        }
        int Contain(Cookie cook, List<Cookie> cookies)
        {
            if (cookies.Count <= 0)
                return -1;
            int i = 0;
            foreach (Cookie cookie in cookies)
            {
                if (cook.name == cookie.name)
                    return i;
                i++;
            }
            return -1;
        }
        List<Cookie> GetActCookie(string ccokie = "")
        {
            string ppath = "https://account.mail.ru/login";
            
            Uri path = new Uri(ppath);
            var request = RequestGET(path, ccokie);
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = true;
            HttpClient client = new HttpClient(httpClientHandler);
            var response = client.Send(request);
            IEnumerable<string> cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            List<Cookie> cookiess = new List<Cookie>();
            foreach (string cookie in cookies)
            {
                Cookie cooks = new Cookie();
                string[] c = cookie.Split("; ")[0].Split("=");
                cooks.name = c[0];
                cooks.value = c[1];
                cookiess.Add(cooks);
            }

            return cookiess;
        }
        HttpRequestMessage RequestPOST(Uri path, HttpContent content, string cookie = "amc_lang=ru_RU")
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = path,
                Method = HttpMethod.Post,
                Content = content
            };

            request.Headers.Add("Host", path.Host);
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:96.0) Gecko/20100101 Firefox/96.0");
            request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Origin", "https://mail.ru");
            request.Headers.Add("Referer", "https://mail.ru/");
            request.Headers.Add("DNT", "1");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Cookie", cookie);
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Sec-Fetch-Dest", "document");
            request.Headers.Add("Sec-Fetch-Mode", "navigate");
            request.Headers.Add("Sec-Fetch-Site", "same-site");
            return request;
        }
        HttpRequestMessage RequestGET(Uri path, string cookie = "amc_lang=ru_RU")
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = path,
                Method = HttpMethod.Get
            };
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:96.0) Gecko/20100101 Firefox/96.0");
            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Origin", "https://mail.ru");
            request.Headers.Add("Referer", "https://mail.ru/");
            request.Headers.Add("DNT", "1");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Cookie", cookie);
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Sec-Fetch-Dest", "document");
            request.Headers.Add("Sec-Fetch-Mode", "navigate");
            request.Headers.Add("Sec-Fetch-Site", "same-site");
            return request;
        }
        public static HttpRequestMessage RequestPOSTos(Uri path, HttpContent content, string cookie = "amc_lang=ru_RU")
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = path,
                Method = HttpMethod.Post,
                Content = content
            };

            request.Headers.Add("Host", "account.my.games");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:96.0) Gecko/20100101 Firefox/96.0");
            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("DNT", "1");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Cookie", cookie);
            request.Headers.Add("Referer", " https://account.my.games/oauth2/?redirect_uri=https%3A%2F%2Fru.warface.com%2Fdynamic%2Fauth%2F%3Fo2%3D1&client_id=ru.warface.com&response_type=code&signup_method=email%2Cphone&signup_social=mailru%2Cfb%2Cvk%2Cg%2Cok%2Ceg%2Ctwitch%2Ctw%2Csteam&lang=ru_RU&gc_id=0.1177");
            request.Headers.Add("Origin", "https://account.my.games");
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Sec-Fetch-Dest", "document");
            request.Headers.Add("Sec-Fetch-Mode", "navigate");
            request.Headers.Add("Sec-Fetch-Site", "same-site");
            return request;
        }
    }
}

