using AutoMapper;
using magix_api.Dtos.Player;
using magix_api.Dtos.Card;

namespace magix_api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Player, GetPlayerDto>();
            CreateMap<AddPlayerDto, Player>();
            CreateMap<UpdatePlayerDto, Player>();
            CreateMap<GetCardDto, Card>();
        }
    }
}