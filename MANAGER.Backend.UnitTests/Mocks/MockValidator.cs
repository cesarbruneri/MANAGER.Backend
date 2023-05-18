using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace MANAGER.Backend.UnitTests.Mocks;

public class MockValidator<T> : Mock<IValidator<T>>
{
    public MockValidator() : base(MockBehavior.Strict) { }

    public MockValidator<T> MockValidate(T user, ValidationResult validationResult)
    {
        Setup(x => x.Validate(user))
            .Returns(validationResult);

        return this;
    }
}
