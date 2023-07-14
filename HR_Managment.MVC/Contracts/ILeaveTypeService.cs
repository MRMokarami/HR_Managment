using HR_Managment.MVC.Models.LeaveType;
using HR_Managment.MVC.Services.Base;

namespace HR_Managment.MVC.Contracts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeVM>> GetAllLeaveTypesAsync();
        Task<LeaveTypeVM> GetLeaveTypeDetailsAsync(int id);
        Task<Response<int>> CreateLeaveTypeAsync(CreateLeaveTypeVM leaveType);
        Task<Response<int>> UpdateLeaveTypeAsync(LeaveTypeVM leaveType);
        Task<Response<int>> DeleteLeaveTypeAsync(int id);
    }
}
