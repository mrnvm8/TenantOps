using TenantOps.Domain.Common;
using System.Net.Mail;

namespace TenantOps.Domain.Identity;

// Represents an Email Address as a Value Object.
// Equality is based on the email value, not identity.
public sealed class Email : ValueObject
{
    // Normalized, immutable email value
    public string Value { get; }

    // Required by ORMs (e.g., EF Core) for materialization
    // Prevents uncontrolled creation
    private Email() { }

    // Public constructor enforces all invariants
    public Email(string value)
    {
        // Business rule: email must not be empty
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Email address is required.");

        // Strong RFC-compliant validation
        // // Business rule: email must follow a valid email structure
        if (!MailAddress.TryCreate(value, out var mailAddress))
            throw new DomainException("Invalid email address.");

        // Normalize for consistent equality and comparison
        Value = mailAddress.Address.ToLowerInvariant();
    }

    // Defines equality for the value object
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    // Improves logging, debugging, and display
    public override string ToString() => Value;

    // Optional factory method to avoid exceptions in workflows
    public static bool TryCreate(string value, out Email email)
    {
        email = null!;

        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!MailAddress.TryCreate(value, out var mailAddress))
            return false;

        email = new Email(mailAddress.Address);
        return true;
    }
}
