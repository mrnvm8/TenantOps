namespace TenantOps.Domain.Common;

public abstract class ValueObject
{
    // Forces derived value objects to specify which fields
    // participate in equality comparison.
    protected abstract IEnumerable<object> GetEqualityComponents();

    // Overrides Object.Equals to implement value-based equality
    public override bool Equals(object? obj)
    {
        // If the other object is null or not a ValueObject,
        // the two objects cannot be equal
        if (obj is not ValueObject other)
            return false;

        // Compares all equality components in order
        // Two value objects are equal if all their components are equal
        return GetEqualityComponents()
            .SequenceEqual(other.GetEqualityComponents());
    }

    // Overrides Object.GetHashCode to match the Equals logic
    public override int GetHashCode()
    {
        // Combines hash codes of all equality components
        // This ensures value objects can be safely used
        // in hash-based collections (Dictionary, HashSet)
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) =>
            {
                // Prevents overflow exceptions
                unchecked
                {
                    // 23 is a commonly used prime number
                    // for generating hash codes
                    return (current * 23) + (obj?.GetHashCode() ?? 0);
                }
            });
    }
}
