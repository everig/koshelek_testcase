﻿using AutoMapper;

namespace Application.Common.Interfaces
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
