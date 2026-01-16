using TenantOps.Domain.Common;

namespace TenantOps.Domain.Attendance;

// Aggregate Root representing a single attendance record
// for one employee on a specific date.
// This aggregate controls clock-in/clock-out rules and attendance state.
public sealed class AttendanceRecord : AggregateRoot
{
    // Identifies the tenant this attendance record belongs to.
    // Enforces multi-tenant isolation.
    public Guid TenantId { get; private set; }

    // Identifies the employee whose attendance is being tracked.
    public Guid EmployeeId { get; private set; }

    // The calendar date for this attendance record.
    // One record per employee per date.
    public DateOnly Date { get; private set; }

    // Time the employee clocked in.
    // Nullable because the employee may be absent or not yet clocked in.
    public TimeOnly? TimeIn { get; private set; }

    // Time the employee clocked out.
    // Nullable until clock-out occurs.
    public TimeOnly? TimeOut { get; private set; }

    // Attendance status for the day (Absent, Present, etc.).
    public AttendanceStatus Status { get; private set; }

    // Required by EF Core for materialization.
    // Prevents uncontrolled creation outside the aggregate.
    private AttendanceRecord() { }

    // Public constructor enforces initial invariants.
    public AttendanceRecord(Guid tenantId, Guid employeeId, DateOnly date)
    {
        // Business rule: attendance must belong to a tenant.
        if (tenantId == Guid.Empty)
            throw new DomainException("TenantId is required.");

        // Business rule: attendance must belong to an employee.
        if (employeeId == Guid.Empty)
            throw new DomainException("EmployeeId is required.");

        TenantId = tenantId;
        EmployeeId = employeeId;
        Date = date;

        // Default state: employee is absent until they clock in.
        Status = AttendanceStatus.Absent;
    }

    // Records the time an employee clocks in.
    // Transitions the attendance status to Present.
    public void ClockIn(TimeOnly time)
    {
        // Business rule: cannot clock in more than once.
        if (TimeIn.HasValue)
            throw new DomainException("Employee has already clocked in.");

        TimeIn = time;
        Status = AttendanceStatus.Present;
    }

    // Records the time an employee clocks out.
    public void ClockOut(TimeOnly time)
    {
        // Business rule: cannot clock out before clocking in.
        if (TimeIn is null)
            throw new DomainException("Cannot clock out before clocking in.");

        // Business rule: cannot clock out more than once.
        if (TimeOut.HasValue)
            throw new DomainException("Employee has already clocked out.");

        // Business rule: clock-out time must be after clock-in time.
        if (time < TimeIn)
            throw new DomainException("Clock-out time cannot be earlier than clock-in time.");

        TimeOut = time;
    }
}
