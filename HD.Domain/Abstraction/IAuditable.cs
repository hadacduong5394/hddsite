using System;

namespace HD.Domain.Abstraction
{
    public interface IAuditable
    {
        string CreateBy { get; set; }

        DateTime? CreateDate { get; set; }

        string UpdateBy { get; set; }

        DateTime? UpdateDate { get; set; }

        bool Status { get; set; }
    }
}