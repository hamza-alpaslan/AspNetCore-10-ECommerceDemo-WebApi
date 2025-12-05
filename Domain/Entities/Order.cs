using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; } = default!;
        public decimal Subtotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Paid;
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
