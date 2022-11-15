using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Andy.X.Portal.Models
{
    public class User
    {
        [Required]
        [Display(Name = "Username")]
        public string Username
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get;
            set;
        }
        public bool RememberLogin
        {
            get;
            set;
        }
        public string ReturnUrl
        {
            get;
            set;
        }
    }
}
