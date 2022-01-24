using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.DataModel
{
    public partial class ArticleTagRelation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? ArticleId { get; set; }
        public int? TagId { get; set; }

        public virtual Article Article { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
