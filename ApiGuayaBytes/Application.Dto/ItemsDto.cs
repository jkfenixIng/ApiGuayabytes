using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities
{
    public class ItemsDto
    {

        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public int Price { get; set; }
        public int IdCategory { get; set; }
    }
}
