namespace RuichenShuxin.AbpPro.DataProtection;

public interface IDataAccessOperateContributor
{
    DataAccessFilterOperate Operate { get; }
    Expression BuildExpression(Expression left, Expression right);
}
