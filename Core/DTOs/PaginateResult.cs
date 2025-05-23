namespace Core.DTOs
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>(); // The paginated items
        public int TotalItems { get; set; } // Total number of items in the dataset
        public int PageNumber { get; set; } // Current page number
        public int PageSize { get; set; } // Number of items per page

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize); // Total number of pages
        public bool HasPreviousPage => PageNumber > 1; // Whether there is a previous page
        public bool HasNextPage => PageNumber < TotalPages; // Whether there is a next page
    }
}
