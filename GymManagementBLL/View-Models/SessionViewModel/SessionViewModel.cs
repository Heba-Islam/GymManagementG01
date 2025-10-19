using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.View_Models.SessionViewModel
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string TrainerName { get; set; } = null!;
        public int Capacity { get; set; }
        public int AvailableSlots { get; set; }

        #region Derived Properties
        public string DateDisplay => $"{StartTime:MMM dd, yyyy}";
        public string TimeRangeDisplay => $"{StartTime:hh:mm tt} - {EndTime:hh:mm tt}";

        public TimeSpan Duration => EndTime - StartTime;

        public string Status
        {
            get
            {
                if (StartTime > DateTime.Now)
                    return "Upcoming";
                else if (StartTime <= DateTime.Now && EndTime >= DateTime.Now)
                    return "Ongoing";
                else
                    return "Completed";
            }

        }
        #endregion
    }
}
