using Microsoft.AspNetCore.Mvc;
using MogulCodeTestBE.Models;
using MogulCodeTestBE.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MogulCodeTestBE.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamCompController : ControllerBase
    {
        private readonly IOverwatchProfileService _overwatchProfileService;

        public TeamCompController(IOverwatchProfileService overwatchProfileService)
        {
            _overwatchProfileService = overwatchProfileService;
        }

        /// <summary>
        /// Sorts the team by highest win ratio (competitive) (try zwitsaltje-2429)
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<PlayerRatio>> GetAsync(string playerOne, string playerTwo, string playerThree, string playerFour, string playerFive, string playerSix)
        {
            string[] players = new string[]{playerOne, playerTwo, playerThree, playerFour, playerFive, playerSix};
            string[] notNullPlayerIds = players.Where(player => player != null).ToArray();
            var playerProfiles = await _overwatchProfileService.GetOverwatchProfiles(notNullPlayerIds);

            return _overwatchProfileService.GetMostLikelyToWin(playerProfiles);
        }

        /// <summary>
        /// Test endpoint to get all profile details (try zwitsaltje-2429)
        /// </summary>
        [HttpGet("{profileId}")]
        public async Task<IEnumerable<OverwatchProfile>> GetProfileAsync(string profileId)
        {
            string[] players = new string[] { profileId };
            string[] notNullPlayerIds = players.Where(player => player != null).ToArray();
            var playerProfiles = await _overwatchProfileService.GetOverwatchProfiles(notNullPlayerIds);

            return playerProfiles;
        }
    }
}
