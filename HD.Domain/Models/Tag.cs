using HD.Domain.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HD.Domain.Models
{
    [Table("Tags")]
    public class Tag : Auditable
    {
        [Key]
        [MaxLength(128)]
        public string TagTitle { get; set; }

        [MaxLength(128)]
        public string TagName { get; set; }
    }
}