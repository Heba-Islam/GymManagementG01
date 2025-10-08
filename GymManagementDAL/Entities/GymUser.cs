using GymManagementDAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal abstract class GymUser:BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateOnly BirthDay { get; set; }

        public Gender Gender { get; set; }

        public Address Address { get; set; }

    }

    [Owned]
    public class Address
    {
        public int BuildingNumber { get; set; }

        public string street { get; set; }

        public string City { get; set; }


    }
}
