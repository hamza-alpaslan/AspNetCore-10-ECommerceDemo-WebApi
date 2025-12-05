using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
namespace Application.DTOs.Product
{
    public record ProductGetDetailDto(int Id,string Name,string? ProductPictureUrl,string? Description, decimal Price,decimal? DiscountedPrice,
        int? Stock, int CategoryId,string CategoryName, double AverageRating, DateTime CreatedDate, DateTime? UpdatedDate);
}