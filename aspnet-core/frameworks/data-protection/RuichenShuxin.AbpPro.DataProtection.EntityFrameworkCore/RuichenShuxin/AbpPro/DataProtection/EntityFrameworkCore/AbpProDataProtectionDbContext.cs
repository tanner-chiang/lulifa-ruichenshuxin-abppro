namespace RuichenShuxin.AbpPro.DataProtection.EntityFrameworkCore;

public abstract class AbpProDataProtectionDbContext<TDbContext> : AbpDbContext<TDbContext>
    where TDbContext : DbContext
{
    public IOptions<AbpProDataProtectionOptions> DataProtectionOptions => LazyServiceProvider.LazyGetRequiredService<IOptions<AbpProDataProtectionOptions>>();
    public ICurrentUser CurrentUser => LazyServiceProvider.LazyGetRequiredService<ICurrentUser>();

    protected AbpProDataProtectionDbContext(DbContextOptions<TDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (LazyServiceProvider != null)
        {
            // TODO: 需要优化表达式树
            //optionsBuilder.AddInterceptors(LazyServiceProvider.GetRequiredService<AbpDataProtectedWriteEntityInterceptor>());
            optionsBuilder.AddInterceptors(LazyServiceProvider.GetRequiredService<AbpProDataProtectedWritePropertiesInterceptor>());
        }
    }

    protected override void ApplyAbpConceptsForAddedEntity(EntityEntry entry)
    {
        base.ApplyAbpConceptsForAddedEntity(entry);
        SetAuthorizationDataProperties(entry);
    }

    protected virtual void SetAuthorizationDataProperties(EntityEntry entry)
    {
        if (entry.Entity is IDataProtected data)
        {
            // TODO: 埋点, 以后可用EF.Functions查询
            //if (data.GetProperty(DataAccessKeywords.AUTH_ROLES) == null)
            //{
            //    data.SetProperty(DataAccessKeywords.AUTH_ROLES, CurrentUser.Roles);
            //}
            //if (data.GetProperty(DataAccessKeywords.AUTH_ORGS) == null)
            //{
            //    data.SetProperty(DataAccessKeywords.AUTH_ORGS, CurrentUser.FindOrganizationUnits());
            //}
        }
    }
}
