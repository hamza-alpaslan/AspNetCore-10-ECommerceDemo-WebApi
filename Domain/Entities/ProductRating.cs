using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ProductRating
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public byte Score { get; set; } //min:1, max:5 
    }
}
