using HR_Managment.MVC.Models.LeaveAllocation;
using HR_Managment.MVC.Services.Base;

namespace HR_Managment.MVC.Contracts
{
    public interface ILeaveAllocationService
    {
        Task<List<LeaveAllocationVM>> GetAllLeaveAllocationsAsync();
        Task<LeaveAllocationVM> GetLeaveAllocationDetailsAsync(int id);
        Task<Response<int>> CreateLeaveAllocationAsync(CreateLeaveAllocationVM leaveAllocation);
        Task<Response<int>> UpdateLeaveAllocationAsync(LeaveAllocationVM  leaveAllocation);
        Task<Response<int>> DeleteLeaveAllocationAsync(int id);
    }
}
