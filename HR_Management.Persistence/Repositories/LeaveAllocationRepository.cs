using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Persistence.Repositories
{
    public class LeaveAllocationRepository:GenericRepository<LeaveAllocation>,ILeaveAllocationRepository
    {
        private readonly LeaveManagementDbContext _context;

        public LeaveAllocationRepository(LeaveManagementDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<LeaveAllocation?> GetLeaveAllocationWithDetailsAsync(int id)
        {
            var leaveAllocation = await _context.LeaveAllocations.Include(t => t.LeaveType)
                .FirstOrDefaultAsync(l=>l.Id==id);
            return leaveAllocation;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync()
        {
            var leaveAllocations = await _context.LeaveAllocations.Include(t => t.LeaveType).ToListAsync();
            return leaveAllocations;
        }
    }
}
