namespace API.Helpers.DTO
{
    public class EditJobDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IEnumerable<int> TagIds { get; set; } = new List<int>();
    }
}
