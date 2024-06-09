using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class RegularUserJobConfig : IEntityTypeConfiguration<RegularUserJob>
    {
        public void Configure(EntityTypeBuilder<RegularUserJob> builder)
        {
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasKey(x => new { x.UserId, x.JobId });

            builder.HasOne(x => x.User).WithMany(x => x.FavoriteJobs).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Job).WithMany(x => x.Followers).HasForeignKey(x => x.JobId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
