using FluentValidation;
using HR_Management.Application.Contracts.Persistence;

namespace HR_Management.Application.DTOs.LeaveRequest.Validators
{
    public class UpdateLeaveRequestValidator:AbstractValidator<UpdateLeaveRequestDto>
    {
        public UpdateLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            Include(new ILeaveRequestValidator(leaveTypeRepository));
            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} is required");
        }
    }
}
