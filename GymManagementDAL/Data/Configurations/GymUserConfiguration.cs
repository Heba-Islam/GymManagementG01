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
    public class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(11);
            builder.HasIndex(x => x.Phone).IsUnique();

            builder.OwnsOne(x => x.Address, address =>
            {

                address.Property(x => x.Street)
                .HasColumnName("Street")
                .HasColumnType("varchar")
                .HasMaxLength(30);

                address.Property(x => x.City)
                .HasColumnName("City")
                .HasColumnType("varchar")
                .HasMaxLength(30);

                address.Property(x => x.BuildingNumber)
                .HasColumnName("BuildingNumber");

            });

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("EmailValidCheck", "Email like '_%@_%._%'");
                tb.HasCheckConstraint("PhoneValidCheck", "Phone like '01%' and Phone not like '%[^0-9]%'");
            });


        }
    }
}
