using AutoMapper;
using HR_Managment.MVC.Contracts;
using HR_Managment.MVC.DTOs.LeaveType;
using HR_Managment.MVC.Models.LeaveType;
using HR_Managment.MVC.Services.Base;

namespace HR_Managment.MVC.Services
{
    public class LeaveTypeService : BaseHttpService,ILeaveTypeService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;

        public LeaveTypeService(ILocalStorageService localStorage, IClient client,IMapper mapper) : base(localStorage, client)
        {
            _client = client;
            _localStorageService = localStorage;
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateLeaveTypeAsync(CreateLeaveTypeVM leaveType)
        {
            try
            {
                var  response = new Response<int>();
                CreateLeaveTypeDto createLeaveTypeDto = _mapper.Map<CreateLeaveTypeDto>(leaveType);

                //todo Authentication

                var apiResponse = await _client.LeaveTypePOSTAsync(createLeaveTypeDto);
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

        public async Task<Response<int>> DeleteLeaveTypeAsync(int id)
        {
            try
            {
                await _client.LeaveTypeDELETEAsync(id);
                return new Response<int>() {IsSuccess = true};
            }
            catch (ApiException e)
            {
                return ConvertApiExceptions<int>(e);
            }
        }

        public async Task<List<LeaveTypeVM>> GetAllLeaveTypesAsync()
        {
            var leaveTypes = await _client.LeaveTypeAllAsync();
            return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
        }

        public async Task<LeaveTypeVM> GetLeaveTypeDetailsAsync(int id)
        {
            var leaveType = await _client.LeaveTypeGETAsync(id);
            return _mapper.Map<LeaveTypeVM>(leaveType);
        }

        public async Task<Response<int>> UpdateLeaveTypeAsync(LeaveTypeVM leaveType)
        {
            try
            {
                UpdateLeaveTypeDto leaveTypeDto = _mapper.Map<UpdateLeaveTypeDto>(leaveType);
                await _client.LeaveTypePUTAsync(leaveTypeDto);
                return new Response<int>() {IsSuccess = true};
            }
            catch (ApiException e)
            {
                return ConvertApiExceptions<int>(e);
            }
        }
    }
}