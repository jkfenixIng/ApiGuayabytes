using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GameTypesDto
    {
        public int IdGameType { get; set; }
        public string GameType { get; set; }
        public int NumberOfDices { get; set; }
        public int StartingAmount { get; set; }
        public int AmountPlayers { get; set; }
        public int MinimumBet { get; set; }

    }
}
