using AutoMapper;
using magix_api.Dtos.PlayerDto;
using magix_api.Dtos.CardDto;
using magix_api.Dtos.DeckDto;
using magix_api.Dtos.FactionDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.TalentDto;


namespace magix_api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Player, GetPlayerDto>();

            CreateMap<Card, GetCardDto>();
            CreateMap<GetCardDto, Card>();

            CreateMap<Faction, GetFactionDto>();
            CreateMap<GetFactionDto, Faction>();

            CreateMap<Hero, GetHeroDto>();
            CreateMap<GetHeroDto, Hero>();

            CreateMap<Talent, GetTalentDto>();
            CreateMap<GetTalentDto, Talent>();
        }
    }
}