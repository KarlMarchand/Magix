namespace magix_api.Dtos
{
    public class PaginatedResponse<T>
    {
        public List<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public PaginatedResponse(List<T> items, int totalItems, int currentPage, int pageSize)
        {
            Items = items;
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            CurrentPage = currentPage;
            PageSize = pageSize;
        }
    }

}
