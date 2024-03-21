using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Configuration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdConfiguration")]
        public int IdConfiguration { get; set; }

        [Required]
        public bool Music { get; set; }

        [Required]
        public bool Sounds { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
