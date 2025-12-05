using Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Cart
{
    public record CartItemDto(ProductCartDto ProductCartDto, int Quantity, decimal LineTotal, decimal TotalLineDicount);

}