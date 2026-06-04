using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace RuichenShuxin.AbpPro.Platform;

public static class PlatformDbContextModelCreatingExtensions
{
    public static void ConfigurePlatform(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Layout>(b =>
        {
            b.ToTable(PlatformDbProperties.DbTablePrefix + "Layouts", PlatformDbProperties.DbSchema);

            b.Property(p => p.Framework)
                .HasMaxLength(PlatformConsts.MaxLength64)
                .HasColumnName(nameof(Layout.Framework))
                .IsRequired();

            b.ConfigureRoute();
        });


        builder.Entity<Menu>(b =>
        {
            b.ToTable(PlatformDbProperties.DbTablePrefix + "Menus", PlatformDbProperties.DbSchema);

            b.ConfigureRoute();

            b.Property(p => p.Framework)
                .HasMaxLength(PlatformConsts.MaxLength64)
                .HasColumnName(nameof(Menu.Framework))
                .IsRequired();
            b.Property(p => p.Component)
                .HasMaxLength(PlatformConsts.MaxLength256)
                .HasColumnName(nameof(Menu.Component))
                .IsRequired();
            b.Property(p => p.Code)
                .HasMaxLength(PlatformConsts.MaxCodeLength)
                .HasColumnName(nameof(Menu.Code))
                .IsRequired();
        });

        builder.Entity<RoleMenu>(x =>
        {
            x.ToTable(PlatformDbProperties.DbTablePrefix + "RoleMenus", PlatformDbProperties.DbSchema);

            x.Property(p => p.RoleName)
                .IsRequired()
                .HasMaxLength(PlatformConsts.MaxLength256)
                .HasColumnName(nameof(RoleMenu.RoleName));

            x.ConfigureByConvention();

            x.HasIndex(i => new { i.RoleName, i.MenuId });
        });

        builder.Entity<UserMenu>(x =>
        {
            x.ToTable(PlatformDbProperties.DbTablePrefix + "UserMenus", PlatformDbProperties.DbSchema);

            x.ConfigureByConvention();

            x.HasIndex(i => new { i.UserId, i.MenuId });
        });

        builder.Entity<UserFavoriteMenu>(x =>
        {
            x.ToTable(PlatformDbProperties.DbTablePrefix + "UserFavoriteMenus", PlatformDbProperties.DbSchema);

            x.Property(p => p.Framework)
                .HasMaxLength(PlatformConsts.MaxLength64)
                .HasColumnName(nameof(Menu.Framework))
                .IsRequired();
            x.Property(p => p.DisplayName)
                .HasMaxLength(PlatformConsts.MaxLength128)
                .HasColumnName(nameof(Route.DisplayName))
                .IsRequired();
            x.Property(p => p.Name)
                .HasMaxLength(PlatformConsts.MaxLength64)
                .HasColumnName(nameof(Route.Name))
                .IsRequired();
            x.Property(p => p.Path)
                .HasMaxLength(PlatformConsts.MaxLength256)
                .HasColumnName(nameof(Route.Path))
                .IsRequired();

            x.Property(p => p.Icon)
                .HasMaxLength(PlatformConsts.MaxLength512)
                .HasColumnName(nameof(UserFavoriteMenu.Icon));
            x.Property(p => p.Color)
                .HasMaxLength(PlatformConsts.MaxLength64)
                .HasColumnName(nameof(UserFavoriteMenu.Color));
            x.Property(p => p.AliasName)
                .HasMaxLength(PlatformConsts.MaxLength128)
                .HasColumnName(nameof(UserFavoriteMenu.AliasName));

            x.ConfigureByConvention();

            x.HasIndex(i => new { i.UserId, i.MenuId });
        });

        builder.Entity<Data>(x =>
        {
            x.ToTable(PlatformDbProperties.DbTablePrefix + "Datas", PlatformDbProperties.DbSchema);

            x.Property(p => p.Code)
                .HasMaxLength(PlatformConsts.MaxLength1024)
                .HasColumnName(nameof(Data.Code))
                .IsRequired();
            x.Property(p => p.Name)
                .HasMaxLength(PlatformConsts.MaxLength64)
                .HasColumnName(nameof(Data.Name))
                .IsRequired();
            x.Property(p => p.DisplayName)
               .HasMaxLength(PlatformConsts.MaxLength128)
               .HasColumnName(nameof(Data.DisplayName))
               .IsRequired();
            x.Property(p => p.Description)
                .HasMaxLength(PlatformConsts.MaxLength1024)
                .HasColumnName(nameof(Data.Description));

            x.ConfigureByConvention();

            x.HasMany(p => p.Items)
                .WithOne()
                .HasForeignKey(fk => fk.DataId)
                .IsRequired();

            x.HasIndex(i => new { i.Name });
        });

        builder.Entity<DataItem>(x =>
        {
            x.ToTable(PlatformDbProperties.DbTablePrefix + "DataItems", PlatformDbProperties.DbSchema);

            x.Property(p => p.DefaultValue)
                .HasMaxLength(PlatformConsts.MaxLength128)
                .HasColumnName(nameof(DataItem.DefaultValue));
            x.Property(p => p.Name)
                .HasMaxLength(PlatformConsts.MaxLength64)
                .HasColumnName(nameof(DataItem.Name))
                .IsRequired();
            x.Property(p => p.DisplayName)
               .HasMaxLength(PlatformConsts.MaxLength128)
               .HasColumnName(nameof(DataItem.DisplayName))
               .IsRequired();
            x.Property(p => p.Description)
                .HasMaxLength(PlatformConsts.MaxLength1024)
                .HasColumnName(nameof(DataItem.Description));

            x.Property(p => p.AllowBeNull).HasDefaultValue(true);

            x.ConfigureByConvention();

            x.HasIndex(i => new { i.Name });
        });


        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();


    }

    public static EntityTypeBuilder<TRoute> ConfigureRoute<TRoute>(
        this EntityTypeBuilder<TRoute> builder)
        where TRoute : Route
    {
        builder
            .Property(p => p.DisplayName)
            .HasMaxLength(PlatformConsts.MaxLength128)
            .HasColumnName(nameof(Route.DisplayName))
            .IsRequired();
        builder
            .Property(p => p.Name)
            .HasMaxLength(PlatformConsts.MaxLength64)
            .HasColumnName(nameof(Route.Name))
            .IsRequired();
        builder
            .Property(p => p.Path)
            .HasMaxLength(PlatformConsts.MaxLength256)
            .HasColumnName(nameof(Route.Path));
        builder
            .Property(p => p.Redirect)
            .HasMaxLength(PlatformConsts.MaxLength256)
            .HasColumnName(nameof(Route.Redirect));

        builder.ConfigureByConvention();

        return builder;
    }
}
