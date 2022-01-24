using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.DataModel
{
    public partial class Tag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tag()
        {
            ArticleTagRelations = new HashSet<ArticleTagRelation>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        public string TagName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual HashSet<ArticleTagRelation> ArticleTagRelations { get; set; }
    }
}
