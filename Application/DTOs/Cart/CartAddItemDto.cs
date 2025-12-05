using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Cart
{
    public record CartAddItemDto(int ProductId,int Quantity);
}
