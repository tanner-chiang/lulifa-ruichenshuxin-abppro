using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace RuichenShuxin.AbpPro.Storage.EntityFrameworkCore;

public static class StorageDbContextModelCreatingExtensions
{
    public static void ConfigureStorage(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(StorageDbProperties.DbTablePrefix + "Questions", StorageDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
    }
}
