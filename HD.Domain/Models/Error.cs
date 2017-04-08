using HD.Domain.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HD.Domain.Models
{
    [Table("Errors")]
    public class Error : Auditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Message { get; set; }

        public string Stacktrace { get; set; }
    }
}