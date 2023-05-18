using MANAGER.Backend.Application.IRepositories;
using MANAGER.Backend.Core.Domain.Users;
using Moq;

namespace MANAGER.Backend.UnitTests.Mocks;

public class MockUserRepository : Mock<IUserRepository>
{
    public MockUserRepository() : base(MockBehavior.Strict) { }

    public MockUserRepository MockAddAsync(User user)
    {
        Setup(x => x.AddAsync(user))
            .Returns(Task.CompletedTask);

        return this;
    }
}
