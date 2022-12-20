using Ardalis.Specification;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Specifications
{
    public class TagsForCategorySpec : Specification<Tag>
    {
        public TagsForCategorySpec(Category? category)
        {
            Query.Where(t => t.Category.Equals(category))
                .AsNoTracking();
        }
    }
}
