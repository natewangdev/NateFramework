using Nate.Core.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nate.Data.EntityFrameworkCore.Sample.Models.Entities
{
    public class User : AuditEntity, IEntity, ISoftDelete
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }
        public string? DeletedBy { get; set; }
        [NotMapped]
        public ICollection<UserAddress> Addresses { get; set; } = [];
    }
}
