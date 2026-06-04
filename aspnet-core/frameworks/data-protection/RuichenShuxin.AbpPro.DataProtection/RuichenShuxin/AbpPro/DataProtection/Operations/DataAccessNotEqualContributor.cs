namespace RuichenShuxin.AbpPro.DataProtection;

public class DataAccessNotEqualContributor : IDataAccessOperateContributor
{
    public DataAccessFilterOperate Operate => DataAccessFilterOperate.NotEqual;

    public Expression BuildExpression(Expression left, Expression right)
    {
        return Expression.NotEqual(left, right);
    }
}
