using FluentValidation;

namespace HR_Management.Application.DTOs.LeaveType.Validators
{
    public class ILeaveTypeDtoValidator: AbstractValidator<ILeaveTypeDto>
    {
        public ILeaveTypeDtoValidator()
        {
            RuleFor(n => n.Name).NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} is required")
                .MaximumLength(200).WithMessage("{PropertyName} should not exceed 200");
            RuleFor(p => p.DefaultDay)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .GreaterThan(0)
                .LessThan(100);



        }
    }
}
