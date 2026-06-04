namespace RuichenShuxin.AbpPro.EntityFrameworkCore;

public static class AbpProDbContextModelCreatingExtensions
{

    public static void ConfigureAbpPro(this ModelBuilder builder)
    {

        builder.Entity<Book>(b =>
        {
            b.ToTable(AbpProDbProperties.DbTablePrefix + "Books", AbpProDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne<Author>().WithMany().HasForeignKey(x => x.AuthorId).IsRequired();
        });
        builder.ConfigureEntityAuth<Book, Guid, BookAuth>();

        builder.Entity<Author>(b =>
        {
            b.ToTable(AbpProDbProperties.DbTablePrefix + "Authors", AbpProDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(AuthorConsts.MaxNameLength);

            b.HasIndex(x => x.Name);
        });

    }

}
