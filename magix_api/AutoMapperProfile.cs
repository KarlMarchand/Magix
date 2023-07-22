using AutoMapper;
using magix_api.Dtos.PlayerDto;
using magix_api.Dtos.CardDto;
using magix_api.Dtos.FactionDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.TalentDto;
using magix_api.Dtos.DeckDto;

namespace magix_api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Player, GetPlayerDto>();
            CreateMap<GameServerPlayerDto, Player>()
                .ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => DateTime.Parse(src.lastLogin)));
            CreateMap<PlayerStat, GetPlayerStatsDto>();

            CreateMap<DeckDto, Deck>();
            CreateMap<Deck, DeckDto>();
            CreateMap<GetDeckDto, Deck>();
            CreateMap<Deck, GetDeckDto>();

            CreateMap<Card, GetCardDto>();
            CreateMap<GetCardDto, Card>();
            CreateMap<DeckCardDto, Card>();

            CreateMap<Faction, GetFactionDto>();
            CreateMap<GetFactionDto, Faction>();

            CreateMap<Hero, GetHeroDto>();
            CreateMap<GetHeroDto, Hero>();

            CreateMap<Talent, GetTalentDto>();
            CreateMap<GetTalentDto, Talent>();
        }
    }
}