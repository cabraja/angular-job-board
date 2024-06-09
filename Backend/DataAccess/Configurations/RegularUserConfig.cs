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
    internal class RegularUserConfig : BaseEntityConfig<RegularUser>
    {
        protected override void CustomConfiguration(EntityTypeBuilder<RegularUser> builder)
        {
            builder.Property(x => x.Username).IsRequired().HasMaxLength(32);
        }
    }
}
