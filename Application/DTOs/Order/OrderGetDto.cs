using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Order
{
    public record OrderGetDto(decimal Subtotal, decimal DiscountTotal, decimal GrandTotal, OrderStatus Status, ICollection<OrderItemDto> Items);
}