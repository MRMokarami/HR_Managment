using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Features.LeaveTypes.Handlers.Commands;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Application.Profiles;
using HR_Management.Application.Responses;
using HR_Management.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR_Management.Application.UnitTests.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILeaveTypeRepository> _leaveTypeRepositoryMock;

        public CreateLeaveTypeCommandHandlerTest()
        {
            _leaveTypeRepositoryMock = MockLeaveRepository.GetLeaveTypeRepository();
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile<MappingProfiles>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task CreateLeaveType()
        {
            var handler = new CreateLeaveTypeCommandHandler(_leaveTypeRepositoryMock.Object,_mapper);
            var result = await handler.Handle(new CreateLeaveTypeCommand()
            {
                LeaveTypeDto = new CreateLeaveTypeDto()
                {
                    Id = 3,
                    DefaultDay = 15,
                    Name = "TestDto"
                }
            },CancellationToken.None);
            result.ShouldBeOfType(typeof(BaseCommandResponse));
            var leaveTypes = await _leaveTypeRepositoryMock.Object.GetAllAsync();
            leaveTypes.Count.ShouldBe(3);
        }
    }
}
