using MANAGER.Backend.Application.IRepositories;
using MANAGER.Backend.Core.Domain.Entities.Users;
using Moq;

namespace MANAGER.Backend.UnitTests.Mocks;

public class MockUserRepository : Mock<IUserRepository>
{
    public MockUserRepository() : base(MockBehavior.Strict) { }

    public MockUserRepository MockAddAsync(User user)
    {
        Setup(x => x.AddAsync(It.Is<User>(x => 
            x.Name == user.Name &&
            x.LastName == user.LastName &&
            x.Email == user.Email &&
            x.Age == user.Age
            )))
            .Returns(Task.CompletedTask);

        return this;
    }
}
