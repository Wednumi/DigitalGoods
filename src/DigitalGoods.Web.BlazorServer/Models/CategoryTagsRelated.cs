using DigitalGoods.Core.Attributes;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Web.BlazorServer.Models
{
    public class CategoryTagsRelated
    {
        public Category? Category { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public Action<Category?> CategoryChanged { get; set; }

        public CategoryTagsRelated(Category? category, ICollection<Tag> tags)
        {
            Category = category;
            Tags = new List<Tag>(tags);
        }

        public void SetCategory(Category? category)
        {
            Category = category;
            if(CategoryChanged is not null)
            {
                CategoryChanged.Invoke(Category);
            }
        }
    }
}
