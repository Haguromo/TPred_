using System.Data.Entity;

namespace Persistence.DataModel
{
    public partial class TPredDB : DbContext
    {
        public TPredDB(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TPredDB, Migrations.Configuration>());
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<ArticleTagRelation> ArticleTagRelations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .Property(e => e.Name)
                .IsUnicode(true);

            modelBuilder.Entity<Article>()
                .Property(e => e.Text)
                .IsUnicode(true);

            modelBuilder.Entity<Article>()
                .Property(e => e.Url)
                .IsUnicode(true);

            modelBuilder.Entity<Article>()
                .Property(e => e.SourceName)
                .IsUnicode(true);

            modelBuilder.Entity<Tag>()
                .Property(e => e.TagName)
                .IsUnicode(true);
        }
    }
}
