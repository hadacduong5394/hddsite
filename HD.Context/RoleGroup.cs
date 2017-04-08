using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HD.Context
{
    [Table("RoleGroups")]
    public class RoleGroup
    {
        [Key]
        [Column(Order = 1)]
        public int GroupId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int RoleId { get; set; }
    }
}