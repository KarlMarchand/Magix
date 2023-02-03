using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using magix_api.Dtos.Player;
using magix_api.Models;


namespace magix_api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Player, GetPlayerDto>();
            CreateMap<AddPlayerDto, Player>();
            CreateMap<UpdatePlayerDto, Player>();
        }
    }
}