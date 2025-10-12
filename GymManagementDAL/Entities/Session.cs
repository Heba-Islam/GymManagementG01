using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    public class Session:BaseEntity
    {
        public string Description { get; set; } = null!;

        public int Capacity { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        #region relationships
        #region category session
        public Category Category { get; set; } = null!;

        public int CategoryId { get; set; }

        #endregion

        #region Trainer session

        public Trainer Trainer { get; set; } = null!;

        public int TrainerId { get; set; }

        #endregion

        #region has many members
        public ICollection<MemberSessions> SessionMembers { get; set; } = null!;

        #endregion

        #endregion

    }
}
