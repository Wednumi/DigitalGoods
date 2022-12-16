using Ardalis.Specification;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Specifications
{
    public class TagByNameSpec : Specification<Tag>
    {
        public TagByNameSpec(string name)
        {
            Query.Where(t => t.Name == name);
        }
    }
}
