using Microsoft.Extensions.Logging;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;

namespace HZY.Services.Admin.WxBot.Http
{
    /// <summary>
    /// ChatGpt服务
    /// </summary>
    public class ChatGptService
    {
        private const string defaultBotReply = "你太厉害了，说的话把我难倒了，我要去学习了，不然没法回答你的问题";

        private readonly ILogger<ChatGptService> _logger;

        public ChatGptService(ILogger<ChatGptService> logger)
        {
            _logger = logger;
          
        }
        /// <summary>
        /// 获取机器人回复
        /// </summary>
        /// <param name="key">天行key</param>
        /// <param name="question">提问（建议urlencode）</param>
        /// <param name="uniqueid">用户唯一身份ID，方便上下文关联</param>
        /// <returns></returns>
        public async Task<string> GetBotReplyAsync(string key, string question, string uniqueid)
        {
            try
            {
                var openAiService = new OpenAIService(new OpenAiOptions()
                {
                    ApiKey = key
                });
                var completionResult = await openAiService.Completions.CreateCompletion(new CompletionCreateRequest()
                {
                    Prompt = question,
                    MaxTokens = 2000,
                    Temperature = 0.9f,
                    User= uniqueid
                }, OpenAI.GPT3.ObjectModels.Models.TextDavinciV3);
                if (completionResult.Successful)
                {
                    return completionResult.Choices.FirstOrDefault().Text;
                }
                else
                {
                    return $"{completionResult.Error.Code}: {completionResult.Error.Message}";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取ChatGpt回复失败");
                return ex.InnerException.ToString();
            }
        }
    }
}
