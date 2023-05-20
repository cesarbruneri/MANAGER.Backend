using MANAGER.Backend.Application.IRepositories;
using MANAGER.Backend.Core.Domain.Entities.Base;
using Moq;

namespace MANAGER.Backend.UnitTests.Mocks;

public class MockBaseRepository<T> : Mock<IBaseRepository<T>> where T : EntityBase
{
    public MockBaseRepository() : base(MockBehavior.Strict) { }

    public MockBaseRepository<T> MockAddAsync(T entity)
    {
        Setup(x => x.AddAsync(It.IsAny<T>()))
            .Returns(Task.CompletedTask);

        return this;
    }
}
