using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using MyDotNetCore.Data;
using MyDotNetCore.Models;
using Newtonsoft.Json;

namespace MyDotNetCore.Services
{
    public class AccessToken
    {
        // 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
        // 返回token示例
        private DataContext _dbContext;
        public AccessToken(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public String TOKEN { get {

                if (_dbContext.Set<BdToken>().Count() > 0)
                {
                    BdToken token = _dbContext.Set<BdToken>().First();
                    if (DateTime.Compare(token.Exexpires_in, DateTime.Now) > 0)
                    {
                        return token.Token;
                    }
                    else
                    {
                        //已过期 移除
                        _dbContext.Set<BdToken>().Remove(token);
                    }
                }

                TokenResJson t = JsonConvert.DeserializeObject<TokenResJson>(GetAccessToken());
                BdToken b = new BdToken
                {
                    Token = t.Access_token,
                    Exexpires_in = DateTime.Now.AddDays(30)
                };
                _dbContext.Set<BdToken>().Add(b);
                _dbContext.SaveChanges();
                return b.Token;
            } }

        // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        private static readonly String clientId = "SnEO6ZblR8xDD5RyYMeaeQ3x";
        // 百度云中开通对应服务应用的 Secret Key
        private static readonly String clientSecret = "mEg6dy7W19QrfZ1GawGjUPe7Te8VW1GS";

        public static String GetAccessToken()
        {
            String authHost = "https://aip.baidubce.com/oauth/2.0/token";
            HttpClient client = new HttpClient();
            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret)
            };
            HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            //Console.WriteLine(result);
            return result;
        }
        public string GetFaceDetect(List<KeyValuePair<string, string>> list)
        {
            String Host = "https://aip.baidubce.com/rest/2.0/face/v2/detect?access_token="+ TOKEN;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");           
            HttpResponseMessage response = client.PostAsync(Host, new FormUrlEncodedContent(list)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            //Console.WriteLine(result);
            return result;
        }
    }
    public class TokenResJson
    {
        public string Access_token { get; set; }
        public int Expires_in { get; set; }
        public string Scope { get; set; }
        public string Session_key { get; set; }
        public string Refresh_token { get; set; }
        public string Session_secret { get; set; }
    }
}