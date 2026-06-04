namespace RuichenShuxin.AbpPro.DataProtection;

public class DataAccessEqualContributor : IDataAccessOperateContributor
{
    public DataAccessFilterOperate Operate => DataAccessFilterOperate.Equal;

    public Expression BuildExpression(Expression left, Expression right)
    {
        return Expression.Equal(left, right);
    }
}
