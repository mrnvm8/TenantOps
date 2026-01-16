namespace TenantOps.Domain.Common;

//This class is a marker and boundary enforcer.
/** 
 * An Aggregate Root:
    Is the only object allowed to be accessed from outside
    Controls all changes to the aggregate
  **/
public abstract class AggregateRoot : Entity
{
    protected AggregateRoot() : base() { }
}

