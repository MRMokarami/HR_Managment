using AutoMapper;
using HR_Managment.MVC.Contracts;
using HR_Managment.MVC.DTOs.LeaveRequest;
using HR_Managment.MVC.Models.LeaveRequest;
using HR_Managment.MVC.Services.Base;

namespace HR_Managment.MVC.Services
{
    public class LeaveRequestService :BaseHttpService,ILeaveRequestService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;

        public LeaveRequestService(ILocalStorageService localStorage, IClient client, ILocalStorageService localStorageService, IMapper mapper) : base(localStorage, client)
        {
            _client = client;
            _localStorageService = localStorageService;
            _mapper = mapper;
        }

        public async Task<LeaveRequestVM> GetLeaveRequestDetailsAsync(int id)
        {
            var leaveRequest = await _client.LeaveRequestGETAsync(id);
            return _mapper.Map<LeaveRequestVM>(leaveRequest);

        }

        public async Task<List<LeaveRequestVM>> GetAllLeaveRequestAsync()
        {
            var leaveRequests = await _client.LeaveRequestAllAsync();
            return _mapper.Map<List<LeaveRequestVM>>(leaveRequests);
        }

        public async Task<Response<int>> CreateLeaveRequestAsync(CreateLeaveRequestVM createLeaveRequestVM)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveRequestDto createRequestTypeDto = _mapper.Map<CreateLeaveRequestDto>(createLeaveRequestVM);

                //todo Authentication

                var apiResponse = await _client.LeaveRequestPOSTAsync(createRequestTypeDto);
                if (apiResponse.IsSuccess)
                {
                    response.Data = apiResponse.Id;
                    response.IsSuccess = true;
                }
                else
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationErrors.Add(error);
                    }

                    response.IsSuccess = false;
                }

                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<Response<int>> UpdateLeaveRequestAsync(LeaveRequestVM updateLeaveRequestVM)
        {
            try
            {
                UpdateLeaveRequestDto leaveAllocationDto = _mapper.Map<UpdateLeaveRequestDto>(updateLeaveRequestVM);
                await _client.LeaveRequestPUTAsync(updateLeaveRequestVM.Id, leaveAllocationDto);
                return new Response<int>() { IsSuccess = true };
            }
            catch (ApiException e)
            {
                return ConvertApiExceptions<int>(e);
            }
        }

        public async Task<Response<int>> DeleteLeaveRequestAsync(int id)
        {
            try
            {
                await _client.LeaveRequestDELETEAsync(id);
                return new Response<int>() { IsSuccess = true };
            }
            catch (ApiException e)
            {
                return ConvertApiExceptions<int>(e);
            }
        }

        public async Task<Response<int>> ChangeLeaveRequestApprove(ChangeLeaveRequestApproveVM changeLeaveRequestApproveVM)
        {
            try
            {
                ChangeLeaveRequestApproveDto leaveTypeDto = _mapper.Map<ChangeLeaveRequestApproveDto>(changeLeaveRequestApproveVM);
                await _client.ChangeApprovalAsync(changeLeaveRequestApproveVM.Id, leaveTypeDto);
                return new Response<int>() { IsSuccess = true };
            }
            catch (ApiException e)
            {
                return ConvertApiExceptions<int>(e);
            }
        }
    }
}
