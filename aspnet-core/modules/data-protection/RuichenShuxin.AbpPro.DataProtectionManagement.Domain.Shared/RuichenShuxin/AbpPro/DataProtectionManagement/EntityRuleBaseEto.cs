namespace RuichenShuxin.AbpPro.DataProtectionManagement;

[Serializable]
public abstract class EntityRuleBaseEto : EntityEto<Guid>, IMultiTenant
{
    public Guid? TenantId { get; set; }
    public bool IsEnabled { get; set; }
    public DataAccessOperation Operation { get; set; }
    public Guid EntityTypeId { get; set; }
    public string EntityTypeFullName { get; set; }
    public DataAccessFilterGroup FilterGroup { get; set; }
}
