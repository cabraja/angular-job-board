using DataAccess.Models;

namespace API.Helpers.DTO
{
    public class SmallJobDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public EmployerPartialDTO? Employer { get; set; }
        public IEnumerable<TagPartialDTO> Tags { get; set; } = new List<TagPartialDTO>();
    }
    public class JobDTO : SmallJobDTO
    {
        public string Description { get; set; } = string.Empty;
    }

    public class EmployerPartialDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class TagPartialDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
