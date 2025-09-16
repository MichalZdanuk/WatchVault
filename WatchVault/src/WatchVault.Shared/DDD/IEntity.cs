namespace WatchVault.Shared.DDD;
public interface IEntity
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
}
