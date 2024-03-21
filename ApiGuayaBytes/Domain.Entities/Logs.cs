using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Logs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int IdAccion { get; set; }

        [ForeignKey("IdAccion")]
        public Acciones Accion { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public DateTime DateLog { get; set; }
    }

}