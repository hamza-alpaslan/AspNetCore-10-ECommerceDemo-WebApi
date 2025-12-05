using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? ProductPictureUrl { set; get; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice {  get; set; }
        public int? Stock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public ICollection<ProductRating> Ratings { get; set; } = new List<ProductRating>();
        public bool IsDiscounted => DiscountedPrice != 0;
        public double AverageRating
            => Ratings.Count == 0 ? 0 : Ratings.Average(r => r.Score);
    }
}
