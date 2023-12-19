﻿using AutoMapper;
using DishesAPI.Entities;
using DishesAPI.Models;

namespace DishesAPI.Profiles;

public class IngredientProfile : Profile
{
    public IngredientProfile()
    {
        CreateMap<Ingredient, IngredientDTO>()
             .ForMember(
                i => i.DishId,
                o => o.MapFrom(s => s.Dishes.First().Id));                
    }
}
