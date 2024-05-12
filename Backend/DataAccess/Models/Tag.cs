using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Tag : BaseEntity
    {
        public enum TagType
        {
            Info,
            Tech
        }
        public string Name { get; set; } = string.Empty;
        public TagType Type { get; set; }
        public IEnumerable<Job> Jobs { get; set; } = new List<Job>();
    }
}
