using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPortal.DataBase
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        public partial class UserMetadata
        {
            [Key]
            [Required(AllowEmptyStrings = false)]
            [StringLength(50, ErrorMessage = "Длина логина должна быть от 1 до 50")]
            [RegularExpression("^[0-9,a-z,A-Z]+$", ErrorMessage = "Логин должен содержать лишь латинские буквы и цифры")]
            public string Username { get; set; }

            [Required(AllowEmptyStrings = false)]
            [StringLength(50, ErrorMessage = "Длина пароля должна быть от 1 до 50")]
            [RegularExpression("^[0-9,a-z,A-Z]+$", ErrorMessage = "Пароль должен содержать лишь латинские буквы и цифры")]
            public string Password { get; set; }

            [Required]
            public UserRole Role { get; set; }

            [Required]
            public bool passwordChanged { get; set; }

            [Required(AllowEmptyStrings = false)]
            [StringLength(50, ErrorMessage = "E-Mail не должен быть длиннее 50 символов")]
            public string EMail { get; set; }

            [InverseProperty("UserServiced")]
            public virtual ICollection<Cancellation> Cancellation { get; set; }
        }
    }
}