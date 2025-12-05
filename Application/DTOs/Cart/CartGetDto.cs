using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Cart
{
    public record CartGetDto(ICollection<CartItemDto> Items,decimal Total,decimal TotalDiscount);
}
