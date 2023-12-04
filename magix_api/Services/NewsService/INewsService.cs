using magix_api.Dtos.NewsDto;

namespace magix_api.Services.NewsService
{
    public interface INewsService
    {
        Task<ServiceResponse<List<GetNewsDto>>> GetNewsAsync();
    }
}
