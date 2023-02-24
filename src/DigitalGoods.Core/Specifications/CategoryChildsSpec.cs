using Ardalis.Specification;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Specifications
{
    internal class CategoryChildsSpec : Specification<Category>
    {
        public CategoryChildsSpec(Category? parent)
        {
            int? parentId = parent?.Id;
            Query.Where(c => c.ParentId == parentId)
                .AsNoTracking();
        }
    }
}
