using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HD.Context
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(128)]
        [Column(TypeName = "varchar")]
        public string Name { get; set; }

        [MaxLength(128)]
        public string Descreption { get; set; }

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