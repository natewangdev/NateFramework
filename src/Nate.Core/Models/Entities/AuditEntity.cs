namespace Nate.Core.Models.Entities
{
    public abstract class AuditEntity<TKey> : BaseEntity<TKey>, IAuditEntity
    {
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public string? LastModifiedBy { get; set; }
    }

    public abstract class AuditEntity : AuditEntity<Guid>
    {
    }
}
