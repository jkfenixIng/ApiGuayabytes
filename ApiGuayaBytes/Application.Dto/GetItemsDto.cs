using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GetItemsDto
    {
        public int IdItem { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public int Price { get; set; }

        public int IdCategory { get; set; }
    }
}
