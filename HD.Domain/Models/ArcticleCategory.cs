using HD.Domain.Abstraction;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HD.Domain.Models
{
    [Table("ArcticleCategories")]
    public class ArcticleCategory : Auditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int? ParentId { get; set; }

        [MaxLength(256)]
        public string CatName { get; set; }

        [MaxLength(256)]
        public string URL { get; set; }

        public virtual ICollection<Arcticle> Articles { get; set;}
    }
}