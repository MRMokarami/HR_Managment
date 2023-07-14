using FluentValidation;
using HR_Management.Application.Contracts.Persistence;

namespace HR_Management.Application.DTOs.LeaveAllocation.Validation
{
    public class ILeaveAllocationValidator:AbstractValidator<ILeaveAllocationDto>
    {
        public ILeaveAllocationValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(p => p.NumberOfDays).GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");
            RuleFor(p => p.Period);
            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0).WithMessage("{PropertyName} must greater than 0")
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MustAsync(async (id, token) =>
                {
                    var leaveTypeExist = await leaveTypeRepository.IsExist(id);
                    return leaveTypeExist;
                });
        }
    }
}
