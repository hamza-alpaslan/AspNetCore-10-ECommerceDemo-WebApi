using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Product
{
    public record ProductUpdateDto(int Id,string Name, string? ProductPictureUrl, string? Description, decimal Price, decimal? DiscountedPrice, int? Stock, int CategoryId);
}
