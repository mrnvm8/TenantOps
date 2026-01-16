using TenantOps.Domain.Attendance;
using TenantOps.Domain.Common;

namespace TenantOps.Domain.Contracts.Repositories;

public interface IAttendanceRecordRepository : IRepository
{
    Task<AttendanceRecord?> GetByIdAsync(Guid recordId);

    Task<AttendanceRecord?> GetByEmployeeAndDateAsync(
        Guid tenantId,
        Guid employeeId,
        DateOnly date);

    Task AddAsync(AttendanceRecord record);
    Task UpdateAsync(AttendanceRecord record);
}
