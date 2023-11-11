using System.ComponentModel.DataAnnotations;

namespace UI.Areas.Admin.Models
{
    public class UserVM
    {
        public UserVM()
        {
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }

    }
}
