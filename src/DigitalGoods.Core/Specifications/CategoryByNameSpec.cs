using Ardalis.Specification;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Specifications
{
    public class CategoryByNameSpec : Specification<Category>
    {
        public CategoryByNameSpec(string name)
        {
            Query.Where(c => c.Name == name);
        }
    }
}
