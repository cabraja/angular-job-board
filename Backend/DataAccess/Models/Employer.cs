using DataAccess.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Employer : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string EmployeeCount { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public List<Job> Jobs { get; set; } = new List<Job>();
    }
}
