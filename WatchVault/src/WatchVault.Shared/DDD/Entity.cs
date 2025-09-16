using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchVault.Shared.DDD;
public abstract class Entity : IEntity
{
    [Column(Order = 0)]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column(Order = 1)]
    public DateTime CreationDate { get; set; }

    [Column(Order = 2)]
    public DateTime UpdateDate { get; set; }
}
