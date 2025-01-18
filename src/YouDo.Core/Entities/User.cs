using Microsoft.AspNetCore.Identity;
using YouDo.Core.Enums;

namespace YouDo.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public EnumGender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public IEnumerable<ToDo> ToDos { get; set; }
    }
}
