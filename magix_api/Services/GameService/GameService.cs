using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using magix_api.Data;

namespace magix_api.Services.GameService
{
    public class GameService : IGameService
    {
        private readonly IMapper _mapper;
        private readonly MagixContext _context;

        public GameService(IMapper mapper, MagixContext context)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}