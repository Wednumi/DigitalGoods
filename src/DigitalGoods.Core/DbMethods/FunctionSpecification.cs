namespace DigitalGoods.Core.DbMethods
{
    public abstract class FunctionSpecification<T>
    {
        public Action Function { get; set; }

        public abstract FormattableString ExecQuery();
    }
}
