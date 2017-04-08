using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HD.Domain.Models
{
    [Table("PostLikes")]
    public class PostLike
    {
        [Key]
        public string Id { get; set; }

        public int ProductId { get; set; }

        public bool IsLike { get; set; }

        public string IPAddress { get; set; }
    }
}