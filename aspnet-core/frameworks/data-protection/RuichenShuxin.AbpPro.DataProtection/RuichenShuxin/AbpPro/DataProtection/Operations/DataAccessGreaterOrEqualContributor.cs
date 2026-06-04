namespace RuichenShuxin.AbpPro.DataProtection;

public class DataAccessGreaterOrEqualContributor : IDataAccessOperateContributor
{
    public DataAccessFilterOperate Operate => DataAccessFilterOperate.GreaterOrEqual;

    public Expression BuildExpression(Expression left, Expression right)
    {
        return Expression.GreaterThanOrEqual(left, right);
    }
}
