using Ardalis.Specification;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Specifications
{
    public class CategoryParentSpec : Specification<Category>
    {
        public CategoryParentSpec(Category category)
        {
            Query.Where(c => c.Id == category.ParentId);
        }
    }
}
