namespace TenantOps.Application.Attendance.Commands.ClockIn;

public sealed record ClockInCommand(
        Guid EmployeeId,
        DateOnly Date,
        TimeOnly Time);