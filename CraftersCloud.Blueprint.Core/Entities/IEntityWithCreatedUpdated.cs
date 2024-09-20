namespace CraftersCloud.Blueprint.Core.Entities;

public interface IEntityWithCreatedUpdated
{
    void SetCreated(DateTimeOffset createdOn, Guid createdBy);

    void SetUpdated(DateTimeOffset updatedOn, Guid updatedBy);
}