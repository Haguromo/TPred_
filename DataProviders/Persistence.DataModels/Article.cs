using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.DataModel
{
    public partial class Article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Article()
        {
            ArticleTagRelations = new HashSet<ArticleTagRelation>();
        }

        public Article(string name, string text, string url, string source)
        {
            Name = name;
            Text = text;
            Url = url;
            SourceName = source;

            Processed = false;

            ArticleTagRelations = new HashSet<ArticleTagRelation>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Text { get; set; }

        [Index(IsUnique = true)]
        [StringLength(777)]
        public string Url { get; set; }

        [Column(TypeName = "ntext")]
        public string SourceName { get; set; }

        [Column(TypeName = "bit")]
        public bool Processed { get; set; }
        // to do - implement "false" default value for this column

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticleTagRelation> ArticleTagRelations { get; set; }
    }
}
