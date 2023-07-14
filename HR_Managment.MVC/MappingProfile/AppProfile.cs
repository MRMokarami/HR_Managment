using AutoMapper;
using HR_Managment.MVC.DTOs.LeaveAllocation;
using HR_Managment.MVC.DTOs.LeaveRequest;
using HR_Managment.MVC.DTOs.LeaveType;
using HR_Managment.MVC.Models.LeaveAllocation;
using HR_Managment.MVC.Models.LeaveRequest;
using HR_Managment.MVC.Models.LeaveType;

namespace HR_Managment.MVC.MappingProfile
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<CreateLeaveTypeDto, CreateLeaveTypeVM>().ReverseMap();
            CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
            CreateMap<UpdateLeaveTypeDto, LeaveTypeVM>().ReverseMap();

            CreateMap<CreateLeaveRequestDto, CreateLeaveRequestVM>().ReverseMap();
            CreateMap<LeaveRequestVM, LeaveRequestDto>().ReverseMap();
            CreateMap<UpdateLeaveRequestDto, LeaveRequestVM>().ReverseMap();
            CreateMap<ChangeLeaveRequestApproveDto, ChangeLeaveRequestApproveVM>().ReverseMap();

            CreateMap<LeaveAllocationVM, LeaveAllocationDto>().ReverseMap();
            CreateMap<CreateLeaveAllocationVM, CreateLeaveAllocationDto>().ReverseMap();
            CreateMap<UpdateLeaveAllocationDto, LeaveAllocationVM>().ReverseMap();
        }
    }
}
