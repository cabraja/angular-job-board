namespace API.Helpers.QueryModels
{
    public class JobQueryModal : PaginatedQueryModal
    {
        public string? Title { get; set; } = string.Empty;
        public int? EmployerId { get; set; }

        public string? TagIds { get; set; } = string.Empty;
    }
}
