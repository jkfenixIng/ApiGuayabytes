using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserInventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdUserInventory")]
        public int IdUserInventory { get; set; }

        [Required]
        public int IdUser { get; set; }

        [Required]
        public int IdItem { get; set; }
        [Required]
        public bool Active { get; set; }
        // Relaciones de clave externa
        [ForeignKey("IdUser")]
        public Users User { get; set; }

        [ForeignKey("IdItem")]
        public Items Item { get; set; }
    }
}
