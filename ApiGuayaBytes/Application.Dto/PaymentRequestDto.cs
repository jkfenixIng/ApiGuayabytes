using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class PaymentRequestDto
    {
        public decimal Amount { get; set; }
        public string? Nonce { get; set; }
    }
}
