using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Product
{
    public record ProductGetDto(int Id, string Name, string? ProductPictureUrl, decimal Price,decimal? DiscountedPrice, bool IsDiscounted, double AverageRating);
}