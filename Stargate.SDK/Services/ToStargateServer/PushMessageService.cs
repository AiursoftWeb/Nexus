using Aiursoft.Handler.Abstract.Exceptions;
using Aiursoft.Handler.Abstract.Models;
using Aiursoft.Scanner.Interfaces;
using Aiursoft.Stargate.SDK.Models.MessageAddressModels;
using Aiursoft.XelNaga.Models;
using Aiursoft.XelNaga.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Aiursoft.Stargate.SDK.Services.ToStargateServer
{
    public class PushMessageService : IScopedDependency
    {
        private readonly HTTPService _httpService;
        private readonly StargateLocator _stargateLocator;
        public PushMessageService(
            HTTPService httpService,
            StargateLocator serviceLocation)
        {
            _httpService = httpService;
            _stargateLocator = serviceLocation;
        }

        public async Task<AiurProtocol> PushMessageAsync(string accessToken, int channelId, string messageContent, bool noexception = false)
        {
            var url = new AiurUrl(_stargateLocator.Endpoint, "Message", "PushMessage", new { });
            var form = new AiurUrl(string.Empty, new PushMessageAddressModel
            {
                AccessToken = accessToken,
                ChannelId = channelId,
                MessageContent = messageContent
            });
            var result = await _httpService.Post(url, form, true);
            var jResult = JsonConvert.DeserializeObject<AiurProtocol>(result);
            if (!noexception && jResult.Code != ErrorType.Success)
            {
                throw new AiurUnexceptedResponse(jResult);
            }
            return jResult;
        }
    }
}
