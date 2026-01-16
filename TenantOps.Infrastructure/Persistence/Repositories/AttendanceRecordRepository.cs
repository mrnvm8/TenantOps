using Microsoft.EntityFrameworkCore;
using TenantOps.Domain.Attendance;
using TenantOps.Domain.Contracts.Repositories;

namespace TenantOps.Infrastructure.Persistence.Repositories;

internal sealed class AttendanceRecordRepository : IAttendanceRecordRepository
{
    private readonly TenantOpsDbContext _context;

    public AttendanceRecordRepository(TenantOpsDbContext context)
    {
        _context = context;
    }

    public Task<AttendanceRecord?> GetByIdAsync(Guid recordId)
    {
        return _context.AttendanceRecords
            .FirstOrDefaultAsync(x => x.Id == recordId);
    }

    public Task<AttendanceRecord?> GetByEmployeeAndDateAsync(
        Guid tenantId,
        Guid employeeId,
        DateOnly date)
    {
        return _context.AttendanceRecords
            .FirstOrDefaultAsync(x =>
                x.TenantId == tenantId &&
                x.EmployeeId == employeeId &&
                x.Date == date);
    }

    public async Task AddAsync(AttendanceRecord record)
    {
        await _context.AttendanceRecords.AddAsync(record);
    }

    public Task UpdateAsync(AttendanceRecord record)
    {
        _context.AttendanceRecords.Update(record);
        return Task.CompletedTask;
    }
}