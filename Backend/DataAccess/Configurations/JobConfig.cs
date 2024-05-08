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
    public class JobConfig : BaseEntityConfig<Job>
    {
        protected override void CustomConfiguration(EntityTypeBuilder<Job> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Description).IsRequired().HasColumnType("text");
        }
    }
}
