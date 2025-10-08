using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configurations
{
    internal class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnType("varchar(50)");

            builder.Property(x => x.Price)
                .HasColumnType("decimal(10,2)");

            builder.Property(x => x.Description)
                .HasColumnType("varchar(200)");

            builder.ToTable(tb => tb.HasCheckConstraint("DurationDaysConstraint",
                "DurationDays between 1 and 365"));

            
        }
    }
}
