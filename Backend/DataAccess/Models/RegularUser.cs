using DataAccess.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class RegularUser : BaseEntity
    {
        public string Username { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<Job> FavoriteJobs { get; set; } = new List<Job>();
    }
}
