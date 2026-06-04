namespace RuichenShuxin.AbpPro.Core;

public static class AbpProCoreConsts
{
    /// <summary>
    /// 应用名称
    /// </summary>
    public const string ApplicationName = "AbpPro";

    public const string ModuleDataProtection = "DataProtection";
    public const string ModulePlatform = "Platform";


    public const int MaxLength64 = 64;

    public const int MaxLength128 = 128;

    public const int MaxLength256 = 256;

    public const int MaxLength512 = 512;

    public const int MaxLength1024 = 1024;

    /// <summary>
    /// 数据库类型相关常量
    /// </summary>
    public static class DatabaseProviderNames
    {
        public const string MySql = "mysql";
        public const string Oracle = "oracle";
        public const string Postgres = "postgres";
        public const string Sqlite = "sqlite";
        public const string SqlServer = "sqlserver";
    }

    /// <summary>
    /// URL相关常量
    /// </summary>
    public static class Urls
    {
        public const string PasswordReset = "Abp.Account.PasswordReset";
        public const string EmailConfirmation = "Abp.Account.EmailConfirmation";
    }

    /// <summary>
    /// 多语言相关常量
    /// </summary>
    public static class Languages
    {
        public const string ZhHans = "zh-Hans";
        public const string EN = "en";
    }

    /// <summary>
    /// Swagger相关常量
    /// </summary>
    public static class Swagger
    {
        public const string ApiTitle = $"{ApplicationName} API";
        public const string Version = "v1";
        public const string JsonEndpoint = $"/swagger/{Version}/swagger.json";
    }

}
