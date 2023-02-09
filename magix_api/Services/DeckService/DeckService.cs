using AutoMapper;
using magix_api.Data;

namespace magix_api.Services.DeckService
{
    public class DeckService : IDeckService
    {
        private readonly IMapper _mapper;
        private readonly MagixContext _context;

        public DeckService(IMapper mapper, MagixContext context)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}