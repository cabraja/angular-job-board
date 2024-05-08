using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Job : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Employer Employer { get; set; }
        public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
    }
}
