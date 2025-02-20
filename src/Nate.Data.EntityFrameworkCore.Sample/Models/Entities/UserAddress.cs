using Nate.Core.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nate.Data.EntityFrameworkCore.Sample.Models.Entities
{
    [Table("UserAddresses")]
    public class UserAddress : AuditEntity, IEntity
    {
        public Guid UserId { get; set; }
        public string Province { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public bool IsDefault { get; set; }

        [NotMapped]
        public string FullAddress { get; set; } = string.Empty;
        [NotMapped]
        public User? User { get; set; }
    }
}
