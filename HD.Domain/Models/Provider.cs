using HD.Domain.Abstraction;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HD.Domain.Models
{
    [Table("Provider")]
    public class Provider : Auditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [MaxLength(128)]
        public string ProviderName { get; set; }

        [MaxLength(128)]
        public string URL { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}