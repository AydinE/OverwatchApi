using MogulCodeTestBE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MogulCodeTestBE.Services
{
    public interface IOverwatchProfileService
    {
        Task<IEnumerable<OverwatchProfile>> GetOverwatchProfiles(IEnumerable<string> battleNetIds);
        IEnumerable<PlayerRatio> GetMostLikelyToWin(IEnumerable<OverwatchProfile> playerProfiles);
    }
}
