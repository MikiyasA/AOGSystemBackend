namespace AOGSystem.Domain;
public class BaseEntity
{
    public Guid Id { get;  set; }
    public DateTime CreatedAT { get;  set; }
    public DateTime? UpdatedAT { get;  set;  }
    public Guid? CreatedBy { get;  set; }
    public Guid? UpdatedBy { get;set; }
}
