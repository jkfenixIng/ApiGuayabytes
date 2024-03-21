using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ConfigGlobal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdConfigGlobal")]
        public int IdConfigGlobal { get; set; }

        [Required]
        public string Config { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
