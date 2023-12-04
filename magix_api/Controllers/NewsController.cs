using magix_api.Dtos.NewsDto;
using magix_api.Services.NewsService;
using Microsoft.AspNetCore.Mvc;

namespace magix_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetNewsDto>>>> GetNews()
        {
            var response = await _newsService.GetNewsAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
