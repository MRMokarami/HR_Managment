using HR_Management.Domain;

namespace HR_Management.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository:IGenericRepository<LeaveRequest>
    {
        Task<LeaveRequest?> GetLeaveRequestWithDetailsAsync(int id);
        Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync();
        Task ChangeApproveStatus(LeaveRequest leaveRequest, bool? approveStatus);
    }
}
