using DigitalGoods.Core.Attributes;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Web.BlazorServer.Models
{
    public class CategoryTagsRelation
    {
        public Category? Category { get; private set; }

        public HashSet<Tag> Tags { get; set; }

        public Func<Category?, Task> CategoryChanged { get; set; }

        public CategoryTagsRelation(Category? category, ICollection<Tag> tags)
        {
            Category = category;
            Tags = new HashSet<Tag>(tags);
        }

        public async Task SetCategory(Category? category)
        {
            Category = category;
            if (CategoryChanged is not null)
            {
                await CategoryChanged(category);
            }    
        }
    }
}
