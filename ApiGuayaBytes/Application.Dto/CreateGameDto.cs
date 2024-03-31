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
    public class CreateGameDto
    {
        public int IdGameType { get; set; }
        public int IdGameModes { get; set; }
    }
}
