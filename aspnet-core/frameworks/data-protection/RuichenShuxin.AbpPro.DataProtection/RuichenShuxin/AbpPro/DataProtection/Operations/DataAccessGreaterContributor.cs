namespace RuichenShuxin.AbpPro.DataProtection;

public class DataAccessGreaterContributor : IDataAccessOperateContributor
{
    public DataAccessFilterOperate Operate => DataAccessFilterOperate.Greater;

    public Expression BuildExpression(Expression left, Expression right)
    {
        return Expression.GreaterThan(left, right);
    }
}
