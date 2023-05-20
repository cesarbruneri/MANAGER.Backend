using FluentAssertions;
using MANAGER.Backend.Core.Domain.Entities.Users;
using MANAGER.Backend.Core.Extensions;
using Xunit;

namespace MANAGER.Backend.UnitTests.Extensions;

public class ObjectExtensionsTests
{
    [Fact]
    public void AsList_ObjectNoList_TransformInList()
    {
        // Arrange
        var @object = Guid.NewGuid();

        // Act
        var result = @object.AsList();

        // Assert
        var expected = new List<Guid>();
        result.GetType().Should().Be(expected.GetType());
    }

    [Fact]
    public void AsList_NullObject_TransformInList()
    {
        // Arrange
        User? @object = null;

        // Act
        var result = @object.AsList();

        // Assert
        var expected = new List<User>();
        result.GetType().Should().Be(expected.GetType());
    }
}
