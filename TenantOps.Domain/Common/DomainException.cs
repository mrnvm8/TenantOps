namespace TenantOps.Domain.Common;

// Represents an exception that originates from the Domain layer.
// This is used to signal violations of business rules or invariants.
/** 
 * eg
 * if (newEmail == Email)
        throw new DomainException("Email address must be different.");
 * **/
public sealed class DomainException : Exception
{
    // Initializes a new instance of the DomainException class
    // with a domain-specific error message.
    public DomainException(string message) : base(message){}
}
