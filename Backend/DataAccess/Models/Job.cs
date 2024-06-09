using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Job : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Employer Employer { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<RegularUserJob> Followers { get; set; } = new List<RegularUserJob>();
    }
}
