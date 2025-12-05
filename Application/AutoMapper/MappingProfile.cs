
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Application.DTOs.Cart;
using AutoMapper;
using Application.DTOs.Product;
using Application.DTOs.Order;
using Application.DTOs.User;

namespace Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        
        public MappingProfile()
        {
            

            //Product
            CreateMap<Product, ProductCreateDto>().ConstructUsing(src => new ProductCreateDto(
                Name:src.Name,
                ProductPictureUrl:src.ProductPictureUrl,
                Description:src.Description,
                Price:src.Price,
                DiscountedPrice:src.DiscountedPrice,
                Stock:src.Stock,
                CategoryId:src.CategoryId
                ));
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductGetDto>().ConstructUsing(src => new ProductGetDto(
                Id:src.Id,
                Name:src.Name,
                ProductPictureUrl:src.ProductPictureUrl,
                Price:src.Price,
                DiscountedPrice:src.DiscountedPrice,
                IsDiscounted:src.IsDiscounted,
                AverageRating:src.AverageRating
                ));
            CreateMap<Product, ProductGetDetailDto>().ConstructUsing(src => new ProductGetDetailDto(
                Id: src.Id,
                Name:src.Name,
                ProductPictureUrl:src.ProductPictureUrl,
                Description:src.Description,
                Price:src.Price,
                DiscountedPrice:src.DiscountedPrice,
                Stock:src.Stock,
                CategoryId:src.CategoryId,
                CategoryName:src.Category.Name,
                AverageRating:src.AverageRating,
                CreatedDate:src.CreatedDate,
                UpdatedDate:src.UpdatedDate
                ));
            CreateMap<Product, ProductUpdateDto>().ConstructUsing(src => new ProductUpdateDto(
                Id:src.Id,
                Name: src.Name,
                ProductPictureUrl: src.ProductPictureUrl,
                Description: src.Description,
                Price: src.Price,
                DiscountedPrice: src.DiscountedPrice,
                Stock: src.Stock,
                CategoryId: src.CategoryId
                ));
            CreateMap<ProductUpdateDto, Product>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore());

            //Cart
            CreateMap<CartItem, CartItemDto>()
                .ConstructUsing((src, ctx) => new CartItemDto(
                    ProductCartDto: ctx.Mapper.Map<ProductCartDto>(src.Product),
                    Quantity: src.Quantity,
                    LineTotal: src.LineTotal,
                    TotalLineDicount: src.TotalLineDicount
                ));


            CreateMap<Cart, CartGetDto>()
                .ConstructUsing((src, ctx) => new CartGetDto(
                    Items: ctx.Mapper.Map<ICollection<CartItemDto>>(src.Items),
                    Total: src.Total,
                    TotalDiscount: src.TotalDiscount
                ));
            //Order
            CreateMap<OrderItem, OrderItemDto>().ConstructUsing(src => new OrderItemDto(
                ProductId:src.ProductId,
                ProductName:src.ProductName,
                UnitPrice:src.UnitPrice,
                DiscountedPrice:src.DiscountedPrice,
                Quantity:src.Quantity,
                LineTotal: src.LineTotal,
                TotalLineDiscount: src.TotalLineDiscount
                ));
            CreateMap<Order, OrderGetDto>().ConstructUsing((src, ctx) => new OrderGetDto(
                Items:ctx.Mapper.Map<ICollection<OrderItemDto>>(src.Items),
                Subtotal:src.Subtotal,
                DiscountTotal:src.DiscountTotal,
                GrandTotal:src.GrandTotal,
                Status:src.Status
                ));
            //User

            CreateMap<ApplicationUser, UserGetDto>().ConstructUsing(src => new UserGetDto(
                Id:src.Id,
                Name:src.Name,
                LastName:src.LastName,
                UserName:src.UserName,
                Email:src.Email
                ));

        }
    }
}
