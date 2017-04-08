using HD.Domain.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HD.Domain.Models
{
    [Table("Articles")]
    public class Arcticle : Auditable
    {
        public Arcticle()
        {
            LikeCount = 0;
            ViewCount = 0;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [MaxLength(256)]
        public string Tittle { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string Descreption { get; set; }

        public string Content { get; set; }

        [MaxLength(512)]
        public string Tags { get; set; }

        public int CatId { get; set; }

        [MaxLength(128)]
        public string Author { get; set; }

        [MaxLength(512)]
        public string ImageUrl { get; set; }

        [MaxLength(512)]
        public string URL { get; set; }

        public int LikeCount { get; set; }

        public int ViewCount { get; set; }

        [MaxLength(256)]
        public string MetaDescreption { get; set; }

        [MaxLength(256)]
        public string MetaKeyword { get; set; }

        [ForeignKey("CatId")]
        public virtual ArcticleCategory ArcticleCat { get; set; }
    }
}