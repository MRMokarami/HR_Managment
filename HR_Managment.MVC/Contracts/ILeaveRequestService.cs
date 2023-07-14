using HR_Managment.MVC.Models.LeaveRequest;
using HR_Managment.MVC.Services.Base;

namespace HR_Managment.MVC.Contracts
{
    public interface ILeaveRequestService
    {
        Task<LeaveRequestVM> GetLeaveRequestDetailsAsync(int id);
        Task<List<LeaveRequestVM>> GetAllLeaveRequestAsync();
        Task<Response<int>> CreateLeaveRequestAsync(CreateLeaveRequestVM  createLeaveRequestVM);

        Task<Response<int>> UpdateLeaveRequestAsync(LeaveRequestVM  updateLeaveRequestVM);
        Task<Response<int>> DeleteLeaveRequestAsync(int id);
        Task<Response<int>> ChangeLeaveRequestApprove(ChangeLeaveRequestApproveVM  changeLeaveRequestApproveVM);

    }
}
