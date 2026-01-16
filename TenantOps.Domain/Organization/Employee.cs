using TenantOps.Domain.Common;
using TenantOps.Domain.Identity;

namespace TenantOps.Domain.Organization;

// Aggregate Root representing an Employee within a Tenant.
// Controls the employee's identity, lifecycle, and user association.
public sealed class Employee : AggregateRoot
{
    // Identifies the tenant this employee belongs to.
    // Enforces multi-tenant boundaries.
    public Guid TenantId { get; private set; }

    // Optional link to a system User account.
    // Null indicates the employee has no user account yet.
    public Guid? UserId { get; private set; }

    // Identifies the department the employee is assigned to.
    public Guid DepartmentId { get; private set; }

    // Employee's given name.
    public string FirstName { get; private set; }

    // Employee's family name.
    public string LastName { get; private set; }

    // Employee's email address represented as a Value Object.
    public Email Email { get; private set; }

    // Current employment status (e.g., Active, Terminated).
    public EmployeeStatus Status { get; private set; }

    // Required by EF Core for materialization.
    // Prevents uncontrolled creation outside the aggregate.
    private Employee() { }

    // Public constructor enforces domain invariants at creation time.
    public Employee(
        Guid tenantId,
        Guid departmentId,
        string firstName,
        string lastName,
        Email email)
    {
        // Business rule: employee must belong to a tenant.
        if (tenantId == Guid.Empty)
            throw new DomainException("TenantId is required.");

        // Business rule: employee must belong to a department.
        if (departmentId == Guid.Empty)
            throw new DomainException("DepartmentId is required.");

        // Business rule: employee must have a first name.
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainException("First name is required.");

        // Business rule: employee must have a last name.
        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainException("Last name is required.");

        // Business rule: email address is mandatory.
        Email = email ?? throw new DomainException("Email is required.");

        TenantId = tenantId;
        DepartmentId = departmentId;
        FirstName = firstName.Trim();
        LastName = lastName.Trim();

        // Default employment status for a newly created employee.
        Status = EmployeeStatus.Active;
    }

    // Links this employee to a system user account.
    // Prevents reassignment once a user is linked.
    public void AssignUser(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new DomainException("UserId is required.");

        if (UserId.HasValue)
            throw new DomainException("User is already assigned.");

        UserId = userId;
    }

    // Terminates the employee.
    // Prevents redundant state transitions.
    public void Terminate()
    {
        if (Status == EmployeeStatus.Terminated)
            throw new DomainException("Employee is already terminated.");

        Status = EmployeeStatus.Terminated;
    }
}
