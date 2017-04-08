using HD.Domain.Abstraction;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HD.Domain.Models
{
    [Table("ProductCategories")]
    public class ProductCategory : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string CatName { get; set; }

        public string URL { get; set; }

        public int TypeId { get; set; }

        [ForeignKey("TypeId")]
        public virtual TypeCategory TypeCategory { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}