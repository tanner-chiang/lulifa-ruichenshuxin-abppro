namespace RuichenShuxin.AbpPro.Storage;

public static class StorageDbProperties
{
    public static string DbTablePrefix { get; set; } = "Storage";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Storage";
}
