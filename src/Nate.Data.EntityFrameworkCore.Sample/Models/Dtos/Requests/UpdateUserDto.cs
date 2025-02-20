using System.ComponentModel.DataAnnotations;

namespace Nate.Data.EntityFrameworkCore.Sample.Models.Dtos.Requests
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "{0} not allow empty")]
        [EmailAddress(ErrorMessage = "{0} incorrect format")]
        public string UserEmail { get; set; }
    }
}
