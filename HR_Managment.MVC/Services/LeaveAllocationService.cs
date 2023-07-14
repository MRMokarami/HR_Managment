using AutoMapper;
using HR_Managment.MVC.Contracts;
using HR_Managment.MVC.DTOs.LeaveAllocation;
using HR_Managment.MVC.Models.LeaveAllocation;
using HR_Managment.MVC.Services.Base;

namespace HR_Managment.MVC.Services
{
    public class LeaveAllocationService : BaseHttpService,ILeaveAllocationService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;

        public LeaveAllocationService(ILocalStorageService localStorage, IClient client, ILocalStorageService localStorageService, IMapper mapper) : base(localStorage, client)
        {
            _client = client;
            _localStorageService = localStorageService;
            _mapper = mapper;
        }

        public async Task<List<LeaveAllocationVM>> GetAllLeaveAllocationsAsync()
        {
            var leaveAllocations = await _client.LeaveAllocationAllAsync();
            return _mapper.Map<List<LeaveAllocationVM>>(leaveAllocations);
        }

        public async Task<LeaveAllocationVM> GetLeaveAllocationDetailsAsync(int id)
        {
            var leaveAllocation =  await _client.LeaveAllocationGETAsync(id);
            return _mapper.Map<LeaveAllocationVM>(leaveAllocation);
        }

        public async Task<Response<int>> CreateLeaveAllocationAsync(CreateLeaveAllocationVM leaveAllocation)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveAllocationDto createLeaveAllocationDto = _mapper.Map<CreateLeaveAllocationDto>(leaveAllocation);

                //todo Authentication

                var apiResponse = await _client.LeaveAllocationPOSTAsync(createLeaveAllocationDto);
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

        public async Task<Response<int>> UpdateLeaveAllocationAsync(LeaveAllocationVM leaveAllocation)
        {
            try
            {
                UpdateLeaveAllocationDto leaveAllocationDto = _mapper.Map<UpdateLeaveAllocationDto>(leaveAllocation);
                await _client.LeaveAllocationPUTAsync(leaveAllocationDto);
                return new Response<int>() { IsSuccess = true };
            }
            catch (ApiException e)
            {
                return ConvertApiExceptions<int>(e);
            }
        }

        public async Task<Response<int>> DeleteLeaveAllocationAsync(int id)
        {
            try
            {
                await _client.LeaveAllocationDELETEAsync(id);
                return new Response<int>() { IsSuccess = true };
            }
            catch (ApiException e)
            {
                return ConvertApiExceptions<int>(e);
            }
        }
    }
}
