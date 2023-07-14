using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HR_Managment.MVC.Models.LeaveType
{
    public class CreateLeaveTypeVM
    {
        [Required]
        public string Name { get; set; }
        [Required, DisplayName("Default number of days")]
        public int DefaultDay { get; set; }
    }
}
