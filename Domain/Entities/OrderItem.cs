using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;

        public decimal UnitPrice { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal;
        public decimal TotalLineDiscount;
    }
}
