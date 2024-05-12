using DataAccess.Models;

namespace API.Helpers.DTO
{
    public class AddJobDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int EmployerId { get; set; }
        public IEnumerable<int> TagIds { get; set; } = new List<int>();
    }
}
