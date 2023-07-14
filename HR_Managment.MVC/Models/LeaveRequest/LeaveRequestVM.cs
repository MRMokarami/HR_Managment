
namespace HR_Managment.MVC.Models.LeaveRequest
{
    public class LeaveRequestVM
    {
        public int Id { get; set; }
        public System.DateTimeOffset StartDate { get; set; }
        public System.DateTimeOffset EndDate { get; set; }
        public int LeaveTypeId { get; set; }
        public System.DateTimeOffset? DateRequested { get; set; }
        public string RequestComments { get; set; }
        public System.DateTimeOffset? DateActioned { get; set; }
        public bool Approved { get; set; }
        public bool Cancelled { get; set; }

    }
}
