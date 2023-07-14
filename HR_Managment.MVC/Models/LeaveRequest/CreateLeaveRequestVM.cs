namespace HR_Managment.MVC.Models.LeaveRequest
{
    public class CreateLeaveRequestVM
    {
        public System.DateTimeOffset StartDate { get; set; }
        public System.DateTimeOffset EndDate { get; set; }
        public int LeaveTypeId { get; set; }
        public System.DateTimeOffset DateRequested { get; set; }
        public string RequestComments { get; set; }
    }
}
