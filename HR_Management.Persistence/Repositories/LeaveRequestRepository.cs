﻿using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Persistence.Repositories
{
    public class LeaveRequestRepository:GenericRepository<LeaveRequest>,ILeaveRequestRepository
    {
        private readonly LeaveManagementDbContext _context;

        public LeaveRequestRepository(LeaveManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<LeaveRequest?> GetLeaveRequestWithDetailsAsync(int id)
        {
            return await _context.LeaveRequests.Include(l => l.LeaveType).FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync()
        {
            var leaveRequests = await _context.LeaveRequests.Include(l => l.LeaveType).ToListAsync();
            return leaveRequests;
        }

        public async Task ChangeApproveStatus(LeaveRequest leaveRequest, bool? approveStatus)
        {
            leaveRequest.Approved = approveStatus;
            _context.Entry(leaveRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
