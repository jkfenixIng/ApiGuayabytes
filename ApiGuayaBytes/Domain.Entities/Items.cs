using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Items
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdItem")]
        public int IdItem { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public byte[] Image { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int IdCategory { get; set; }
    }
}
