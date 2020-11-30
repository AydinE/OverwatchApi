using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MogulCodeTestBE.Models
{
    public class OverwatchProfile
    {
        public string Name { get; set; }
        public int Endorsement { get; set; }
        public int GamesWon { get; set; }
        public int Level { get; set; }
        public int Prestige { get; set; }
        public bool Private { get; set; }
        public int Rating { get; set; }
        public Stats CompetitiveStats { get; set; }
        public Stats QuickPlayStats { get; set; }
    }
}
