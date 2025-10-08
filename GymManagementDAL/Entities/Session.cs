using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    internal class Session:BaseEntity
    {
        public string Description { get; set; }

        public int Capacity { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        #region relationships
        #region category session
        public Category Category { get; set; }

        public int CategoryId { get; set; }

        #endregion

        #region Trainer session

        public Trainer Trainer { get; set; }

        public int TrainerId { get; set; }

        #endregion

        #region has many members
        [Required]
        public ICollection<MemberSessions> SessionMembers { get; set; }

        #endregion

        #endregion

    }
}
