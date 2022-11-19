using System.ComponentModel.DataAnnotations;

namespace DigitalGoods.Web.BlazorServer.Models
{
    public class Credential
    {
        [Required]
        [MinLength(3, ErrorMessage = "Minimum username length is 3")]
        [Display(Name = "Username")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Minimum password length is 8")]
        public string Password { get; set; } = string.Empty;
    }
}
