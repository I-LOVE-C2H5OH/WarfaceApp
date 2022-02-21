using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyGamesRegger.Data
{

    class AuthMyGames : AuthWarfaceAcc
    {

        private List<Cookie> cookieMyGames = new List<Cookie>();
        private List<Cookie> cookieWarface = new List<Cookie>();
        private string FirstName = "";
        public AuthMyGames(string login, string password)
        {
            cookieWarface = GetCookieWarface(POSTauth(login, password));
        }
        public List<Cookie> GetWarfaceCookies()
        {
            return this.cookieWarface;
        }
        public string GetFisrstName()
        {
            return this.FirstName;
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
            HttpContent content = new StringContent("");
            var request = RequestGET(HttpMethod.Get, new Uri(path), "");
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
            request.Headers.Host = "ru.warface.com";
            HttpResponseMessage response = client.Send(request);
            List<Cookie> cookieWarface = new List<Cookie>();
            IEnumerable<string> cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;

            if (response.StatusCode == HttpStatusCode.BadGateway || (response.Headers.Location != null && response.Headers.Location.OriginalString.Contains("validate")))
                throw new Exception("Превышено к-во входов");



            foreach (string cook in cookies)
            {
                Cookie cooks = new Cookie();
                string[] c = cook.Split("; ")[0].Split("=");
                cooks.name = c[0];
                cooks.value = c[1];
                cookieWarface.Add(cooks);
            }


            return cookieWarface;
        }
        string POSTauth(string log, string pass)
        {
            string ret = "";
            HttpContent content = new StringContent($"login=mexicofarm-04%40xvisual.net&password=fau15wd2&continue=https%3A%2F%2Faccount.my.games%2Foauth2%2F%3Fredirect_uri%3Dhttps%253A%252F%252Fru.warface.com%252Fdynamic%252Fauth%252F%253Fo2%253D1%26client_id%3Dru.warface.com%26response_type%3Dcode%26signup_method%3Demail%252Cphone%26signup_social%3Dmailru%252Cfb%252Cvk%252Cg%252Cok%252Ceg%252Ctwitch%252Ctw%252Csteam%26lang%3Dru_RU%26gc_id%3D0.1177&failure=https%3A%2F%2Faccount.my.games%2Foauth2%2Flogin%2F%3Fcontinue%3Dhttps%253A%252F%252Faccount.my.games%252Foauth2%252Flogin%252F%253Fcontinue%253Dhttps%25253A%25252F%25252Faccount.my.games%25252Foauth2%25252F%25253Fredirect_uri%25253Dhttps%2525253A%2525252F%2525252Fru.warface.com%2525252Fdynamic%2525252Fauth%2525252F%2525253Fo2%2525253D1%252526client_id%25253Dru.warface.com%252526response_type%25253Dcode%252526signup_method%25253Demail%2525252Cphone%252526signup_social%25253Dmailru%2525252Cfb%2525252Cvk%2525252Cg%2525252Cok%2525252Ceg%2525252Ctwitch%2525252Ctw%2525252Csteam%252526lang%25253Dru_RU%252526gc_id%25253D0.1177%2526client_id%253Dru.warface.com%2526lang%253Dru_RU%2526signup_method%253Demail%25252Cphone%2526signup_social%253Dmailru%25252Cfb%25252Cvk%25252Cg%25252Cok%25252Ceg%25252Ctwitch%25252Ctw%25252Csteam%2526gc_id%253D0.1177%26client_id%3Dru.warface.com%26lang%3Dru_RU%26signup_method%3Demail%252Cphone%26signup_social%3Dmailru%252Cfb%252Cvk%252Cg%252Cok%252Ceg%252Ctwitch%252Ctw%252Csteam%26gc_id%3D0.1177&nosavelogin=0&g-recaptcha-response=");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var request = RequestPOST(HttpMethod.Post, new Uri("https://auth-ac.my.games/sign_in"), content);
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;

            HttpClient client = new HttpClient(httpClientHandler);
            HttpResponseMessage response = client.Send(request);

            var cookiessss = Redirecres(response.Headers.Location);

            return cookiessss;
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
        string Redirecres(Uri? path, List<Cookie>? cookies = null)
        {
            //https://account.my.games/oauth2/login/?continue=https%3A%2F%2Faccount.my.games%2Foauth2%2F%3Fredirect_uri%3Dhttps%253A%252F%252Fru.warface.com%252Fdynamic%252Fauth%252F%253Fo2%253D1%26client_id%3Dru.warface.com%26response_type%3Dcode%26signup_method%3Demail%252Cphone%26signup_social%3Dmailru%252Cfb%252Cvk%252Cg%252Cok%252Ceg%252Ctwitch%252Ctw%252Csteam%26lang%3Dru_RU%26gc_id%3D0.1177&client_id=ru.warface.com&lang=ru_RU&signup_method=email%2Cphone&signup_social=mailru%2Cfb%2Cvk%2Cg%2Cok%2Ceg%2Ctwitch%2Ctw%2Csteam&gc_id=0.1177
            string str = "amc_lang=ru_RU";
            if (cookieMyGames != null)
                foreach (Cookie dtr in cookieMyGames)
                {
                    str += "; " + dtr.name + "=" + dtr.value;
                }
            var request = RequestGET(HttpMethod.Get, path, str);
            request.Headers.Host = path.Host;
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
            var response = client.Send(request);
            IEnumerable<string> cookiesss = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            if (cookiesss != null)
            {
                foreach (string cook in cookiesss)
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
            }
            if (response.StatusCode == HttpStatusCode.Found)
            {
                if (response.Headers.Location.AbsoluteUri == "https://account.my.games/")
                    return "";
                return Redirecres(response.Headers.Location, cookies);
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
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
            else
                return "";
        }
        string GETauth(Uri path, List<string> cookies)
        {
            HttpContent content = new StringContent("");
            string str = "amc_lang=ru_RU";
            foreach (string dtr in cookies)
            {
                str += "; " + dtr;
            }
            var request = RequestGET(HttpMethod.Get, path, str);
            request.Headers.Host = path.Host;
            HttpClientHandler httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
            var response = client.Send(request);
            IEnumerable<string> cookiesss = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            if (cookiesss != null)
            {
                List<Cookie> inpit = new List<Cookie>();
                List<Cookie> output = new List<Cookie>();
                foreach (string cookie in cookiesss) //обрабатываем куки пришедшие
                {
                    string cooks = cookie.Split("; ")[0];
                    string[] CookTypaName = cooks.Split("=");
                    Cookie inp = new Cookie();
                    inp.name = CookTypaName[0];
                    inp.value = CookTypaName[1];
                    inpit.Add(inp);
                }
                foreach (string cookie in cookies) // обрабатывыает куки отосланные
                {
                    string cooks = cookie.Split("; ")[0];
                    string[] CookTypaName = cooks.Split("=");
                    Cookie outp = new Cookie();
                    outp.name = CookTypaName[0];
                    outp.value = CookTypaName[1];
                    output.Add(outp);
                }
                for (int i = 0; i < inpit.Count; i++) // изменение куки
                {
                    for (int j = 0; j < output.Count; j++)
                    {
                        if (inpit[i].name == output[j].name)
                        {
                            inpit[i].value = output[j].value;
                        }
                    }
                }
                List<Cookie> newCookie = new List<Cookie>();
                List<string> oldCookieName = new List<string>();
                foreach (Cookie cooks in output)
                {
                    oldCookieName.Add(cooks.name);
                }
                // Проверка новых куки
                foreach (Cookie cooks in inpit)
                {
                    if (!oldCookieName.Contains(cooks.name))
                    {
                        output.Add(cooks);
                    }
                }
                cookies.Clear();
                foreach (Cookie cooks in output)
                {
                    cookies.Add($"{cooks.name}={cooks.value}");
                }

            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stream = response.Content.ReadAsStream();
                StreamReader reader = new StreamReader(stream);
                string text = reader.ReadToEnd();
                Regex regex = new Regex("<input type=\"hidden\" name=\"csrfmiddlewaretoken\" value=\"(.*?)\">");
                MatchCollection matches = regex.Matches(text);
                string csrfmiddlewaretoken = matches[0].Groups[1].Value;
                regex = new Regex("<a href=\"//profile.my.games\" target=\"_blank\">(.*?)</a></span>");
                matches = regex.Matches(text);
                FirstName = matches[0].Groups[1].Value;
                string datas = $"csrfmiddlewaretoken={csrfmiddlewaretoken}&response_type=code&client_id=ru.warface.com&redirect_uri=https%3A%2F%2Fru.warface.com%2Fdynamic%2Fauth%2F%3Fo2%3D1&scope=&state=&hash=be7ced8c2ae834813f503822e744fade&gc_id=0.1177&force=1";
                HttpContent contentes = new StringContent(datas);
                contentes.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                string ccc = "amc_lang=ru_RU";
                foreach (string cook in cookies)
                {
                    ccc += "; " + cook;
                }
                var getwarface = RequestPOSTos(HttpMethod.Post, new Uri("https://account.my.games/oauth2/"), contentes, ccc);
                HttpClientHandler httpClientHandlers = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
                httpClientHandlers.AllowAutoRedirect = false;

                HttpClient clients = new HttpClient(httpClientHandler);
                HttpResponseMessage responses = client.Send(getwarface);

                IEnumerable<string> scookies = responses.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
                List<string> sssscookies = new List<string>();
                foreach (string cookie in scookies)
                {
                    sssscookies.Add(cookie.Split("; ")[0]);
                }
                if (responses.Headers.Location != null)
                    if (responses.Headers.Location.AbsoluteUri.Contains("ru.warface.com"))
                    {
                        return responses.Headers.Location.AbsoluteUri;
                    }



            }
            else if (response.StatusCode == HttpStatusCode.Found)
            {
                Uri? ABSURI = response.Headers.Location;
                if (ABSURI != null)
                {
                    HttpContent responseContent = response.Content;
                    if (ABSURI.AbsoluteUri.Contains("ru.warface.com"))
                    {

                    }
                    return GETauth(ABSURI, cookies);
                }
            }
            return "";

        }
        HttpRequestMessage RequestPOST(HttpMethod httpMethod, Uri path, HttpContent content, string cookie = "amc_lang=ru_RU")
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = path,
                Method = httpMethod,
                Content = content
            };

            request.Headers.Add("Host", "auth-ac.my.games");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:96.0) Gecko/20100101 Firefox/96.0");
            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Origin", "https:account.my.games");
            request.Headers.Add("DNT", "1");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Cookie", cookie);
            request.Headers.Add("Referer", "https://account.my.games");
            request.Headers.Add("Origin", "https://account.my.games");
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Sec-Fetch-Dest", "document");
            request.Headers.Add("Sec-Fetch-Mode", "navigate");
            request.Headers.Add("Sec-Fetch-Site", "same-site");
            return request;
        }
        HttpRequestMessage RequestPOSTos(HttpMethod httpMethod, Uri path, HttpContent content, string cookie = "amc_lang=ru_RU")
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = path,
                Method = httpMethod,
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
        HttpRequestMessage RequestGET(HttpMethod httpMethod, Uri path, string cookie = "amc_lang=ru_RU")
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = path,
                Method = httpMethod
            };

            request.Headers.Add("Host", path.Host);
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

