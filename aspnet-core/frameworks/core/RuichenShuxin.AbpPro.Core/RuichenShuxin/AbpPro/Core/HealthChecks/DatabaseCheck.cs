namespace RuichenShuxin.AbpPro.Core;

public class DatabaseCheck : IHealthCheck, ITransientDependency
{
    protected readonly IIdentityRoleRepository IdentityRoleRepository;

    public DatabaseCheck(IIdentityRoleRepository identityRoleRepository)
    {
        IdentityRoleRepository = identityRoleRepository;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await IdentityRoleRepository.GetListAsync(sorting: nameof(IdentityRole.Id), maxResultCount: 1, cancellationToken: cancellationToken);

            return HealthCheckResult.Healthy($"Could connect to database and get record.");
        }
        catch (Exception e)
        {
            return HealthCheckResult.Unhealthy($"Error when trying to get database record. ", e);
        }
    }
}
