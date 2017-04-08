using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HD.Domain.Abstraction
{
    public abstract class Auditable : IAuditable
    {
        [MaxLength(128)]
        [Column(TypeName = "varchar")]
        public string CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        [MaxLength(128)]
        [Column(TypeName = "varchar")]
        public string UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool Status { get; set; }
    }
}