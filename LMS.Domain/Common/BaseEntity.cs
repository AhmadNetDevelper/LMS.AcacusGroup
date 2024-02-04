using System.ComponentModel.DataAnnotations;

namespace LMS.Domain.Common;

public abstract class BaseEntity
{
    [Key]
    public long Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int? UpdatedBy { get; set; }
}