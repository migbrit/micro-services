﻿using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using AutoMapper;

namespace Discount.Grpc.Mapper
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
