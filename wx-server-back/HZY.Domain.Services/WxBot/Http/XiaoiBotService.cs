using HZY.Infrastructure;
using HZY.Models.Enums;
using HZY.Models.VO.TianXing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HZY.Domain.Services.WxBot.Http
{
    /// <summary>
    /// 小i机器人服务
    /// </summary>
    public class XiaoiBotService
    {
        private const string defaultBotReply = "你太厉害了，说的话把我难倒了，我要去学习了，不然没法回答你的问题";

        public HttpClient _client { get; }
        private readonly ILogger<XiaoiBotService> _logger;

        public XiaoiBotService(HttpClient client, ILogger<XiaoiBotService> logger)
        {
            _logger = logger;
            client.BaseAddress = new Uri("http://nlp.xiaoi.com/robot/webrobot");
            // GitHub API versioning
            client.DefaultRequestHeaders.Add("Accept",
                "application/json");
            _client = client;
        }
        /// <summary>
        /// 获取机器人回复
        /// </summary>
        /// <param name="question">提问（建议urlencode）</param>
        /// <param name="uniqueid">用户唯一身份ID，方便上下文关联</param>
        /// <returns></returns>
        public async Task<string> GetBotReplyAsync(string question, string uniqueid)
        {
            try
            {
                var parmars = new Dictionary<string, string>(3);
                string data = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    sessionId = uniqueid,
                    robotId = "webbot",
                    userId = uniqueid,
                    body = new
                    {
                        content = question
                    },
                    type = "txt"

                });
                parmars.Add("callback", "__webrobot_processMsg");
                parmars.Add("data", data);
                parmars.Add("ts", new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds().ToString());
                var jObject = await GetAsync("", parmars);
                if (jObject == null) return defaultBotReply;
                return jObject == null ? defaultBotReply : jObject["body"]["content"].ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取机器人回复失败");
            }
            return defaultBotReply;
        }

        private async Task<JObject> GetAsync(string url, Dictionary<string, string> parmars)
        {
            url += "?";
            foreach (var item in parmars)
                url += $"{item.Key}={System.Web.HttpUtility.UrlEncode(item.Value)}&";

            HttpResponseMessage response = await _client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("小i机器人接口返回:" + result);
            result = result.Split(";")
                .ToList()
                .Where(t => t.Contains("content"))
                .OrderByDescending(t => t.Length)
                .FirstOrDefault();
            result = result.Replace("__webrobot_processMsg(", "");
            result = result.Remove(result.Length - 1, 1);
            JObject jObject = JObject.Parse(result);
            return response.IsSuccessStatusCode ? jObject : null;
        }

    }
}
