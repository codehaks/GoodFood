using GoodFood.Application.Contracts;
using GoodFood.Application.Models;
using GoodFood.Application.Services;
using GoodFood.Domain.Entities;
using GoodFood.Domain.Values;

namespace GoodFood.Application.Mappers;
public static class UserMapper
{
    public static CustomerInfo MapUserToCustomer(UserInfo userInfo)
    {
        return new CustomerInfo(userInfo.UserId, userInfo.UserName);
    }
}

public static class CartMapper
{
    public static CartDto MapToDto(Cart cart)
    {
        return new CartDto
        {
            Id = cart.Id,
            Customer = cart.Customer,

            Lines = cart.Lines.Select(line => new CartLineModel
            {
                UserId = cart.Customer.UserId,
                UserName = cart.Customer.UserName,
                FoodId = line.FoodId,
                FoodName = line.FoodName,
                FoodDescription = line.FoodDescription,
                FoodImagePath = line.FoodImagePath,
                Quantity = line.Quantity,
                Price = line.Price
            }).ToList(),

            TimeCreated = cart.TimeCreated,
            TimeUpdated = cart.TimeUpdated
        };
    }
}
