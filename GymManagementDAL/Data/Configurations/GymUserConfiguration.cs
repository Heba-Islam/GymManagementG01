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
    internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnType("varchar(50");

            builder.Property(x => x.Email)
                .HasColumnType("varchar(100)");

            builder.ToTable(tb => tb.HasCheckConstraint("EmailValidation",
                "Email like '_%@_%._%'"));

            builder.HasIndex(tb => tb.Email)
                .IsUnique();

            builder.Property(x => x.Phone)
                .HasColumnType("varchar(11)");

            builder.ToTable(tb => tb.HasCheckConstraint("PhoneValidation",
                "Phone like '01[0125]' and Phone not like '%[^0-9]%'"));

            builder.HasIndex(tb => tb.Phone)
                .IsUnique();

            builder.OwnsOne(x => x.Address, AddBuilder =>
            {
                AddBuilder.Property(x => x.City)
                .HasColumnType("varchar(30");

                AddBuilder.Property(x => x.street)
                .HasColumnType("varchar(30");


            }
            );
        }
    }
}
