using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Product
{
    public record ProductCartDto(int Id, string Name, decimal Price, decimal? DiscountedPrice, string? ProductPictureUrl);
}
