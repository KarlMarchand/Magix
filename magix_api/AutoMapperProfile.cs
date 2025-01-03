using AutoMapper;
using magix_api.Dtos;
using magix_api.Dtos.CardDto;
using magix_api.Dtos.DeckDto;
using magix_api.Dtos.FactionDto;
using magix_api.Dtos.GameDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.PlayerDto;
using magix_api.Dtos.TalentDto;

namespace magix_api;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Player, GetPlayerDto>();
        CreateMap<GameServerPlayerDto, Player>()
            .ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => DateTime.Parse(src.lastLogin)));
        CreateMap<PlayerStat, GetPlayerStatsDto>();

        CreateMap<CreateDeckDto, Deck>();
        CreateMap<Deck, CreateDeckDto>();
        CreateMap<GetDeckDto, Deck>();
        CreateMap<Deck, GetDeckDto>();

        CreateMap<Card, GetCardDto>();
        CreateMap<GetCardDto, Card>();
        CreateMap<DeckCardDto, Card>();
        CreateMap<CardFromGameServerDto, DeckCardDto>();
        CreateMap<CardFromGameServerDto, Card>();

        CreateMap<Faction, GetFactionDto>();
        CreateMap<GetFactionDto, Faction>();

        CreateMap<Hero, GetHeroDto>();
        CreateMap<GetHeroDto, Hero>();

        CreateMap<Talent, GetTalentDto>();
        CreateMap<GetTalentDto, Talent>();

        CreateMap<Game, GameResultDto>();
        CreateMap<GameStateFromServerDto, GameStateDto>()
            .ForMember(dest => dest.Hero, opt => opt.MapFrom<HeroClassToHeroResolver>());
        CreateMap<OpponentGameStateFromServerDto, OpponentGameStateDto>()
            .ForMember(dest => dest.Hero, opt => opt.MapFrom<HeroClassToHeroResolver>());
        CreateMap<LatestActionFromServerDto, LatestActionDto>();
        CreateMap<ActionDetailsFromServerDto, ActionDetailsDto>();

        CreateMap(typeof(PaginatedResponse<>), typeof(PaginatedResponse<>))
            .ConvertUsing(typeof(PaginatedResponseConverter<,>));
    }

    public class PaginatedResponseConverter<TSource, TDestination> : ITypeConverter<PaginatedResponse<TSource>, PaginatedResponse<TDestination>>
    {
        private readonly IMapper _mapper;

        public PaginatedResponseConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public PaginatedResponse<TDestination> Convert(PaginatedResponse<TSource> source, PaginatedResponse<TDestination> destination, ResolutionContext context)
        {
            // Map the individual items
            var mappedItems = _mapper.Map<List<TDestination>>(source.Items);

            // Create a new PaginatedResponse with the mapped items and other pagination details
            return new PaginatedResponse<TDestination>(
                mappedItems,
                source.TotalItems,
                source.CurrentPage,
                source.PageSize
            );
        }
    }

    public class HeroClassToHeroResolver : IValueResolver<BaseGameStateFromServerDto, BaseGameStateDto, GetHeroDto?>
    {
        public GetHeroDto? Resolve(BaseGameStateFromServerDto source, BaseGameStateDto destination, GetHeroDto? destMember, ResolutionContext context)
        {
            var hero = new Hero
            {
                Name = source.HeroClass
            };
            hero.ToFrontend();
            return new GetHeroDto { Name = hero.Name };
        }
    }
}