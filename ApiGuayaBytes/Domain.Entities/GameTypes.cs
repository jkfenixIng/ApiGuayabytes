using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GameTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdGameType")]
        public int IdGameType { get; set; }
        [Required]
        public string GameType { get; set; }
        [Required]
        public int NumberOfDices { get; set; }
        [Required]
        public int StartingAmount { get; set; }
        [Required]
        public int AmountPlayers { get; set; }
        [Required]
        public int MinimumBet { get; set; }
    }
}
