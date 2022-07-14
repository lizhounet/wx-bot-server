using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZY.Services.Admin.WxBot.Http
{
    public class GeFeiXunBotService
    {
        public HttpClient _client { get; }
        private readonly ILogger<GeFeiXunBotService> _logger;
        public GeFeiXunBotService(HttpClient client, ILogger<GeFeiXunBotService> logger)
        {
            _logger = logger;
            client.BaseAddress = new Uri("http://127.0.0.1:8888");
            // GitHub API versioning
            client.DefaultRequestHeaders.Add("Accept",
                "application/json");
            _client = client;
        }
        /// <summary>
        /// 获取登录二维码
        /// </summary>
        /// <returns></returns>

        public async Task<string> GetLoginQrCodeAsync()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                type = "get_qr"
            });
            JObject jObject = await PostAsync(json);
            return jObject?.ToString();

        }
        /// <summary>
        /// 检测是否扫码成功
        /// </summary>
        /// <param name="salt">获取登录二维码时的返回值</param>
        /// <returns></returns>
        public async Task<string> CheckLoginAsync(string salt)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                type = "check_login",
                salt
            });
            JObject jObject = await PostAsync(json);
            return jObject?.ToString();

        }

        private async Task<JObject> PostAsync(string json)
        {
            HttpResponseMessage response = await _client.PostAsync("", new StringContent(json, Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            JObject jObject = JObject.Parse(result);
            return response.IsSuccessStatusCode ? jObject : null;
        }

    }
}
