using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Cart : BaseEntity
    {
        public string UserId { get; set; } = default!;
        public virtual ApplicationUser User { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal Total { get; set; }
        public decimal TotalDiscount { get; set; }
    }
}
