using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MogulCodeTestBE.Models
{
    public class Games
    {
        public int Played { get; set; }

        public int Won { get; set; }
        public double Ratio { get { return (double)Won / Played; } }
    }
}
