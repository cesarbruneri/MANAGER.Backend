namespace MANAGER.Backend.Core.Domain.Entities.Base
{
    public class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
