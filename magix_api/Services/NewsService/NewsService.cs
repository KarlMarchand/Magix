using magix_api.Dtos.NewsDto;
using magix_api.utils;

namespace magix_api.Services.NewsService
{
    public class NewsService : INewsService
    {
        public async Task<ServiceResponse<List<GetNewsDto>>> GetNewsAsync()
        {
            var response = new ServiceResponse<List<GetNewsDto>>();
            var result = await GameServerAPI.CallApi<List<GetNewsDto>>("news");

            if (result.IsValid)
            {
                response.Data = result.Content;
            }
            else
            {
                response.Success = false;
                response.Message = result.Error!;
            }

            return response;
        }
    }
}
