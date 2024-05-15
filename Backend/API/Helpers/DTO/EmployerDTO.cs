using DataAccess.Models;

namespace API.Helpers.DTO
{
    public class SmallEmployerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EmployeeCount { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

    }
    public class EmployerDTO : SmallEmployerDTO
    {
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<PartialJobDTO> Jobs { get; set; } = new List<PartialJobDTO>();
    }

    public class PartialJobDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
