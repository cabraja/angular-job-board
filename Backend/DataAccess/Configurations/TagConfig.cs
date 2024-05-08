using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class TagConfig : BaseEntityConfig<Tag>
    {
        protected override void CustomConfiguration(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(24);
            builder.Property(x => x.Type).IsRequired().HasMaxLength(10).HasConversion<string>();
        }
    }
}
