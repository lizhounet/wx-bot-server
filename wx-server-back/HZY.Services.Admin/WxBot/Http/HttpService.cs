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

namespace HZY.Services.Admin.WxBot.Http
{
    /// <summary>
    /// http服务
    /// </summary>
    public class HttpService
    {
        private HttpClient _client { get; }
        private readonly ILogger<HttpService> _logger;

        public HttpService(HttpClient client, ILogger<HttpService> logger)
        {
            _logger = logger;
            _client = client;
        }
        public async Task<string> GetAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            HttpResponseMessage response = await _client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"{url}返回:{result}");
            return result;
        }

    }
}
