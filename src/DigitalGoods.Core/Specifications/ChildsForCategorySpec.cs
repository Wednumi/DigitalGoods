using Ardalis.Specification;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Specifications
{
    internal class ChildsForCategorySpec : Specification<Category>
    {
        public ChildsForCategorySpec(int? parentId)
        {
            Query.Where(c => c.ParentId == parentId)
                .AsNoTracking();
        }
    }
}
