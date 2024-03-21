using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HistoryGames
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdhistoryGame")]
        public int IdhistoryGame { get; set; }

        [Required]
        public int IdUser { get; set; }

        [Required]
        public int IdGame { get; set; }

        [Required]
        public bool IsWinner { get; set; }
    }
}
