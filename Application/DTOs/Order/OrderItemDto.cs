using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Order
{
    public record OrderItemDto(int ProductId,string ProductName, decimal UnitPrice, decimal? DiscountedPrice, int Quantity, decimal LineTotal, decimal TotalLineDiscount);
}
