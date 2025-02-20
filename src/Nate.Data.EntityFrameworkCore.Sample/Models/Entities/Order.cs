using Nate.Core.Models.Entities;

namespace Nate.Data.EntityFrameworkCore.Sample.Models.Entities
{
    public class Order : AuditEntity, ISoftDelete
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }
        public string DeletedBy { get; set; }
    }
}
