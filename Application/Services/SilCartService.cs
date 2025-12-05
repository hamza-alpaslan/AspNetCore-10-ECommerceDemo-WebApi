using Application.DTOs.Cart;
using Application.DTOs.Product;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public  class SilCartService
    {

        private readonly IMapper _mapper;

        public SilCartService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<CartGetDto> GetCart(string userId)
        {
            var cart = new Cart
            {
                Id = 1,
                UserId = "user-123",
                Items = new List<CartItem>
            {
                new CartItem
                {
                    Id = 1,
                    ProductId = 1,
                    Quantity = 2,
                },
                new CartItem
                {
                    Id = 2,
                    ProductId = 2,
                    Quantity = 3
                }
            }
            };

            var cartDto = _mapper.Map<CartGetDto>(cart);

            ProductCreateDto productCreateDto = new ProductCreateDto("Urun1","Url","Description",10.78m,null,45,1);
            Product product=_mapper.Map<Product>(productCreateDto);

            return cartDto;
        }
    }
}