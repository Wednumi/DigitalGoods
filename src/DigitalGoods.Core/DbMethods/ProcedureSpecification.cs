namespace DigitalGoods.Core.DbMethods
{
    public abstract class ProcedureSpecification
    {
        public Action Procedure { get; set; }

        public abstract FormattableString ExecQuery();
    }
}
