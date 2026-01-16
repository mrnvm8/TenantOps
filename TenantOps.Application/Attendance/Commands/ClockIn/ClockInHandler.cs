using TenantOps.Application.Common.Interfaces;
using TenantOps.Domain.Attendance;
using TenantOps.Domain.Contracts.Repositories;

namespace TenantOps.Application.Attendance.Commands.ClockIn;

public sealed class ClockInHandler
{
    private readonly IAttendanceRecordRepository _attendanceRepository;
    private readonly ITenantProvider _tenantProvider;
    private readonly IUnitOfWork _unitOfWork;

    public ClockInHandler(
        IAttendanceRecordRepository attendanceRepository,
        ITenantProvider tenantProvider,
        IUnitOfWork unitOfWork)
    {
        _attendanceRepository = attendanceRepository;
        _tenantProvider = tenantProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(
        ClockInCommand command,
        CancellationToken cancellationToken = default)
    {
        var record =
            await _attendanceRepository.GetByEmployeeAndDateAsync(
                _tenantProvider.TenantId,
                command.EmployeeId,
                command.Date);

        if (record is null)
        {
            record = new AttendanceRecord(
                _tenantProvider.TenantId,
                command.EmployeeId,
                command.Date);

            record.ClockIn(command.Time);
            await _attendanceRepository.AddAsync(record);
        }
        else
        {
            record.ClockIn(command.Time);
            await _attendanceRepository.UpdateAsync(record);
        }

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}