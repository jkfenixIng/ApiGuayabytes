using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdUser")]
        public int IdUser { get; set; }

        public string Name { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int? Cash { get; set; } = 0;
    }
}