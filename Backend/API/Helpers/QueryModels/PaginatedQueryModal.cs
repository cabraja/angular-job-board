namespace API.Helpers.QueryModels
{
    public class PaginatedQueryModal
    {
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 10;
    }
}
