using System.ComponentModel.DataAnnotations;

namespace DigitalGoods.Web.BlazorServer.Models
{
    public class CategoryModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Too short name")]
        public string Name { get; set; }
    }
}
