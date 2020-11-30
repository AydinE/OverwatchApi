using Microsoft.Extensions.Configuration;
using MogulCodeTestBE.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MogulCodeTestBE.Services
{
    public class OverwatchProfileService : IOverwatchProfileService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public OverwatchProfileService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<IEnumerable<OverwatchProfile>> GetOverwatchProfiles(IEnumerable<string> battleNetIds)
        {
            HttpClient client = _clientFactory.CreateClient();
            var tasks = battleNetIds.Select(id => {
                return client.GetAsync($"{_configuration.GetValue<string>("ApiUrl")}{id}/complete");
            });
            var apiResponses = await Task.WhenAll(tasks);

            var apiResponseContentTasks = apiResponses.Select(resp => {
                var parsedResponse = resp.IsSuccessStatusCode ? resp.Content.ReadAsAsync<OverwatchProfile>() : Task.FromResult<OverwatchProfile>(null);
                return parsedResponse;
            });
            return await Task.WhenAll(apiResponseContentTasks);
        }

        public IEnumerable<PlayerRatio> GetMostLikelyToWin(IEnumerable<OverwatchProfile> playerProfiles)
        {
            var playerRatios = new List<PlayerRatio>();
            foreach (var player in playerProfiles)
            {
                var playerRatio = new PlayerRatio {
                    Ratio = player.CompetitiveStats.Games.Ratio,
                    Name = player.Name
                };
                playerRatios.Add(playerRatio);
            }
            return playerRatios.OrderByDescending( p => p.Ratio).ToList();
        }
    }
}
