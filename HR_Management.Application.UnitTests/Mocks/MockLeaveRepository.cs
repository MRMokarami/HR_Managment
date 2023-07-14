using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using Moq;

namespace HR_Management.Application.UnitTests.Mocks
{
    public static class MockLeaveRepository
    {
        private static List<LeaveType> _leaveTypes = new List<LeaveType>()
        {
            new LeaveType
            {
                Id = 1,
                DefaultDay = 10,
                Name = "TestVacation"
            },
            new LeaveType
            {
                Id = 2,
                DefaultDay = 15,
                Name = "Test Sick"
            },

        };
        public static Mock<ILeaveTypeRepository> GetLeaveTypeRepository()
        {
            

            var mockRepository = new Mock<ILeaveTypeRepository>();
            mockRepository.Setup(s => s.GetAllAsync()).ReturnsAsync(_leaveTypes);
            mockRepository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync((int x) => _leaveTypes[x]);
            mockRepository.Setup(s => s.AddAsync(It.IsAny<LeaveType>())).ReturnsAsync((LeaveType lt) =>
            {
                _leaveTypes.Add(lt);
                return lt;
            });
            mockRepository.Setup(s => s.UpdateAsync(It.IsAny<LeaveType>()));
        
            return mockRepository;
        }
    }
}
