using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace MANAGER.Backend.UnitTests.Mocks;

public class MockValidator<T> : Mock<IValidator<T>> where T : class
{
    public MockValidator() : base(MockBehavior.Strict) { }

    public MockValidator<T> MockValidate(List<ValidationFailure>? errors)
    {
        Setup(x => x.Validate(It.IsAny<T>()))
            .Returns(new ValidationResult
            {
                Errors = errors ?? null,
            });

        return this;
    }
}
