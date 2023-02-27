using Microsoft.AspNetCore.Mvc;
using magix_api.Services.DeckService;

namespace magix_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeckController : ControllerBase
    {
        private readonly IDeckService _deckService;

        public DeckController(IDeckService deckService)
        {
            _deckService = deckService;
        }

    }
}