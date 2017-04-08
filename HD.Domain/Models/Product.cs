using HD.Domain.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace HD.Domain.Models
{
    [Table("Products")]
    public class Product: Auditable
    {
        public Product()
        {
            LikeCount = 0;
            ViewCount = 0;
            Quantity = 0;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar"), MaxLength(128)]
        public string BarCode { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string ImageUrl { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        [MaxLength(256)]
        public string Title { get; set; }

        [MaxLength(512)]
        public string Descreption { get; set; }

        public string Content { get; set; }

        [MaxLength(512)]
        public string DigitalInfo { get; set; }

        [MaxLength(512)]
        public string URL { get; set; }

        public decimal? RootPrice { get; set; }

        public decimal? wholesalePrice { get; set; }

        public decimal? RetailPrice { get; set; }

        public decimal? PromotionPrice { get; set; }

        public int Quantity { get; set; }

        public int ProviderId { get; set; }

        public int LikeCount { get; set; }

        public int ViewCount { get; set; }

        [MaxLength(256)]
        public string MetaDescreption { get; set; }

        [MaxLength(256)]
        public string MetaKeyword { get; set; }

        [MaxLength(512)]
        public string Tags { get; set; }

        public int CatId { get; set; }

        [ForeignKey("CatId")]
        public virtual ProductCategory ProductCat { get; set; }

        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }
    }
}