namespace MANAGER.Backend.Core.Domain.Base
{
    public class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
