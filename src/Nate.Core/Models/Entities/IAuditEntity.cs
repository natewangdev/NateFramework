namespace Nate.Core.Models.Entities
{
    public interface IAuditEntity
    {
        DateTime CreatedTime { get; set; }
        string CreatedBy { get; set; }
        DateTime? LastModifiedTime { get; set; }
        string? LastModifiedBy { get; set; }
    }
}
