using MANAGER.Backend.Application.IRepositories;
using MANAGER.Backend.Core.Domain.Entities.Users;
using Moq;

namespace MANAGER.Backend.UnitTests.Mocks;

public class MockUserRepository : Mock<IUserRepository>
{
    public MockUserRepository() : base(MockBehavior.Strict) { }

    #region Mock

    public MockUserRepository MockAddAsync(User user)
    {
        Setup(x => x.AddAsync(It.Is<User>(x => 
            x.Name == user.Name &&
            x.LastName == user.LastName &&
            x.Email == user.Email &&
            x.Password == user.Password
            )))
            .Returns(Task.CompletedTask);

        return this;
    }

    public MockUserRepository MockFindByEmailAsync(string email, User? user)
    {
        Setup(x => x.FindByEmailAsync(email))
            .ReturnsAsync(user);

        return this;
    }

    #endregion

    #region Verify

    public MockUserRepository VerifyFindByEmailAsync(string email, Times times)
    {
        Verify(x => x.FindByEmailAsync(email), times);

        return this;
    }

    #endregion
}
