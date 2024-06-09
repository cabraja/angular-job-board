using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class RegularUserJob
    {
        public int UserId { get; set; }
        public RegularUser User { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
