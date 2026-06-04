namespace RuichenShuxin.AbpPro.CAP;

[DependsOn(
    typeof(AbpEventBusModule),
    typeof(AbpProCoreModule)
    )]
public class AbpProCAPEventBusModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var platformCapOptions = configuration.GetOptions<PlatformCapOptions>();

        context.Services.AddTransient<IFailedThresholdCallbackNotifier, FailedThresholdCallbackNotifier>();
        context.Services.AddSingleton<ISubscribeInvoker, AbpProCAPSubscribeInvoker>();
        context.Services.AddSingleton<ISerializer, AbpProCapSerializer>();

        // 配置CAP
        context.Services.AddCap(options => ConfigureCAP(options, configuration, platformCapOptions));


    }

    private static void ConfigureCAP(CapOptions options, IConfiguration configuration, PlatformCapOptions platformCapOptions)
    {
        options.UseDashboard();

        options.DefaultGroupName = platformCapOptions.EventBus.DefaultGroupName;
        options.GroupNamePrefix = platformCapOptions.EventBus.GroupNamePrefix;
        options.Version = platformCapOptions.EventBus.Version;
        options.FailedRetryInterval = platformCapOptions.EventBus.FailedRetryInterval;
        options.FailedRetryCount = platformCapOptions.EventBus.FailedRetryCount;

        if (platformCapOptions.IsEnabled)
        {
            options.UseMySql(opt =>
            {
                opt.ConnectionString = configuration.GetConnectionString("Default");
            })
            .UseRabbitMQ(opt =>
            {
                opt.HostName = platformCapOptions.RabbitMQ.HostName;
                opt.Port = platformCapOptions.RabbitMQ.Port;
                opt.UserName = platformCapOptions.RabbitMQ.UserName;
                opt.Password = platformCapOptions.RabbitMQ.Password;
                opt.ExchangeName = platformCapOptions.RabbitMQ.ExchangeName;
                opt.VirtualHost = platformCapOptions.RabbitMQ.VirtualHost;
            });
        }
        else
        {
            options.UseInMemoryStorage().UseRedis(platformCapOptions.Redis.Configuration);
        }

        options.FailedThresholdCallback = async (failed) =>
        {
            var exceptionNotifier = failed.ServiceProvider.GetService<IFailedThresholdCallbackNotifier>();
            if (exceptionNotifier != null)
            {
                await exceptionNotifier.NotifyAsync(
                    new AbpProCAPExecutionFailedException(failed.MessageType, failed.Message));
            }
        };

    }

}
